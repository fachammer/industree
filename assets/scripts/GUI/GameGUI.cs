using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Texture2D pauseDialog;
	public Texture2D winDialog;
	public Texture2D loseDialog;
	public Texture2D bilanceDecoration;
	public Color bilanceAirColor;
	public Color bilancePollutionColor;
	public Vector2 bilanceSize;

	public Player[] players;
	public Texture2D selectedActionIconOverlay;

	private GameController gameController;
	private InputManager inputManager;
	private Pollutable pollutable;
	private Planet planet;

    private Rect bilanceAirRectangle;
    private Rect bilancePollutionRectangle;
	private Texture2D bilanceAirTexture;
    private Texture2D bilancePollutionTexture;

    private int[] selectedActionIndices;
    private Rect[][] playersInteractiveIconRectangles;

	private const float BILANCE_TOP_OFFSET = 35;
	private const float DIALOG_TOP_OFFSET = 200;
	private const float ACTION_TOP_OFFSET = 100;

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();
		GameObject planetObject = GameObject.FindGameObjectWithTag(Tags.planet);
		pollutable = planetObject.GetComponent<Pollutable>();
		planet = planetObject.GetComponent<Planet>();

		inputManager.PlayerSelect += OnPlayerSelect;

		bilanceAirRectangle = new Rect((Screen.width - bilanceSize.x) / 2, BILANCE_TOP_OFFSET, bilanceSize.x, bilanceSize.y);
        bilancePollutionRectangle = new Rect((Screen.width - bilanceSize.x) / 2, BILANCE_TOP_OFFSET, bilanceSize.x, bilanceSize.y);

        bilanceAirTexture = Utilities.MakeTexture2DWithColor(bilanceAirColor);
        bilancePollutionTexture = Utilities.MakeTexture2DWithColor(bilancePollutionColor);

        selectedActionIndices = new int[players.Length];
        playersInteractiveIconRectangles = new Rect[players.Length][];

        for(int i = 0; i < playersInteractiveIconRectangles.Length; i++){
        	playersInteractiveIconRectangles[i] = new Rect[players[i].interactiveList.Count];
        	for(int j = 0; j < playersInteractiveIconRectangles[i].Length; j++){
        		playersInteractiveIconRectangles[i][j] = calculateInteractiveIconRectangle(i, j);
        	}
        }
	}

	private void OnPlayerSelect(int playerIndex, float direction){
		if(direction > 0){
			if(selectedActionIndices[playerIndex] > 0){
				selectedActionIndices[playerIndex]--;
			}
		}
		else {
			if(selectedActionIndices[playerIndex] < players[playerIndex].interactiveList.Count - 1){
				selectedActionIndices[playerIndex]++;
			}
		}
	}

	private Rect calculateInteractiveIconRectangle(int playerIndex, int interactiveIndex){
		Player player = players[playerIndex];
		float iconSize = players[playerIndex].interactiveList[interactiveIndex].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
		return new Rect(iconXOffset, ACTION_TOP_OFFSET + iconSize * interactiveIndex, iconSize, iconSize);
	}

	private void OnGUI(){
		DrawBilance();
		DrawActions();
		DrawCredits();

		DrawPauseDialogIfGamePaused();
		DrawGameEndDialogIfGameEnded();
	}

	private void DrawBilance(){
		float pollution =  Mathf.Clamp(pollutable.currentPollution, 0, planet.air);
        bilancePollutionRectangle.width = bilanceAirRectangle.width * pollution / planet.air;

		GUI.DrawTexture(bilanceAirRectangle, bilanceAirTexture, ScaleMode.StretchToFill);
        GUI.DrawTexture(bilancePollutionRectangle, bilancePollutionTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect((Screen.width - bilanceDecoration.width) / 2 + 3, 0, bilanceDecoration.width, bilanceDecoration.height), bilanceDecoration);  
    }

    private void DrawActions(){
    	for(int i = 0; i < players.Length; i++){
    		DrawPlayerActions(i);
    		GUI.DrawTexture(playersInteractiveIconRectangles[i][selectedActionIndices[i]], selectedActionIconOverlay);
    	}
    }

    private void DrawPlayerActions(int playerIndex){
        for (int i = 0; i < players[playerIndex].interactiveList.Count; i++){
        	DrawPlayerAction(playerIndex, i);
        }
    }

    private void DrawPlayerAction(int playerIndex, int interactiveIndex){
    	Rect interactiveIconRect = playersInteractiveIconRectangles[playerIndex][interactiveIndex];
    	Interactive interactive = players[playerIndex].interactiveList[interactiveIndex];

        GUI.DrawTexture(interactiveIconRect, interactive.icon);

        float costXOffset = (players[playerIndex].side == Player.Side.left ? interactiveIconRect.width + 10: -50);
        GUI.Label(new Rect(
            interactiveIconRect.x + costXOffset,
            interactiveIconRect.y + interactiveIconRect.height / 2,
            50, 50), interactive.cost.ToString());
    }

    private void DrawCredits(){

    }

    private void DrawPauseDialogIfGamePaused(){
		if(gameController.GamePaused && !gameController.GameEnded){
			DrawDialog(pauseDialog);
		}
    }

    private void DrawDialog(Texture2D dialog){
        GUI.DrawTexture(new Rect(
        	(Screen.width - dialog.width) / 2, 
        	DIALOG_TOP_OFFSET, 
        	dialog.width, 
        	dialog.height), dialog);
    }

    private void DrawGameEndDialogIfGameEnded(){
    	if(gameController.GameEnded){
        	if(gameController.GameWon){
        		DrawDialog(winDialog);
        	}
        	else {
        		DrawDialog(loseDialog);
        	}
        }
    }
}
