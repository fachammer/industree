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
	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;
	public Texture2D cooldownActionIconOverlay;
	public Texture2D creditsIcon;

	private GameController gameController;
	private InputManager inputManager;
	private Pollutable pollutable;
	private Planet planet;

    private Rect bilanceAirRectangle;
    private Rect bilancePollutionRectangle;
	private Texture2D bilanceAirTexture;
    private Texture2D bilancePollutionTexture;

    private int[] selectedActionIndices;
    private Rect[][] playersActionIconRectangles;
    private bool[][] drawActionDeniedOverlay;
    private Timer[][] actionDeniedOverlayTimers;
    private Timer[][] actionCooldownOverlayTimers;

	private const float BILANCE_TOP_OFFSET = 35;
	private const float DIALOG_TOP_OFFSET = 200;
	private const float ACTION_TOP_OFFSET = 100;
	private const float CREDITS_TOP_OFFSET = 70;

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();
		GameObject planetObject = GameObject.FindGameObjectWithTag(Tags.planet);
		pollutable = planetObject.GetComponent<Pollutable>();
		planet = planetObject.GetComponent<Planet>();

		inputManager.PlayerActionInput += OnPlayerActionInput;
		inputManager.PlayerSelectInput += OnPlayerSelectInput;

		bilanceAirRectangle = new Rect((Screen.width - bilanceSize.x) / 2, BILANCE_TOP_OFFSET, bilanceSize.x, bilanceSize.y);
        bilancePollutionRectangle = new Rect((Screen.width - bilanceSize.x) / 2, BILANCE_TOP_OFFSET, bilanceSize.x, bilanceSize.y);

        bilanceAirTexture = Utilities.MakeTexture2DWithColor(bilanceAirColor);
        bilancePollutionTexture = Utilities.MakeTexture2DWithColor(bilancePollutionColor);

        selectedActionIndices = new int[players.Length];
        playersActionIconRectangles = new Rect[players.Length][];

        for(int i = 0; i < playersActionIconRectangles.Length; i++){
        	playersActionIconRectangles[i] = new Rect[players[i].actionList.Count];
        	for(int j = 0; j < playersActionIconRectangles[i].Length; j++){
        		playersActionIconRectangles[i][j] = calculateActionIconRectangle(i, j);
        	}
        }

        drawActionDeniedOverlay = new bool[players.Length][];

        for(int i = 0; i < drawActionDeniedOverlay.Length; i++){
        	drawActionDeniedOverlay[i] = new bool[players[i].actionList.Count];
        }

        actionDeniedOverlayTimers = new Timer[players.Length][];

        for(int i = 0; i < actionDeniedOverlayTimers.Length; i++){
        	actionDeniedOverlayTimers[i] = new Timer[players[i].actionList.Count];
        }

        actionCooldownOverlayTimers = new Timer[players.Length][];

        for(int i = 0; i < actionDeniedOverlayTimers.Length; i++){
        	actionCooldownOverlayTimers[i] = new Timer[players[i].actionList.Count];
        }
	}

	private void OnPlayerSelectInput(int playerIndex, float selectDirection){
		if(selectDirection > 0){
			if(selectedActionIndices[playerIndex] > 0){
				selectedActionIndices[playerIndex]--;
			}
		}
		else {
			if(selectedActionIndices[playerIndex] < players[playerIndex].actionList.Count - 1){
				selectedActionIndices[playerIndex]++;
			}
		}
	}

	private void OnPlayerActionInput(int playerIndex, float actionDirection){
		Player player = players[playerIndex];
		int actionIndex = selectedActionIndices[playerIndex];
		Action action = player.actionList[actionIndex];

		bool actionSuccessful = player.ActIfPossible(action, actionDirection);

		if(actionSuccessful){
			actionCooldownOverlayTimers[playerIndex][actionIndex] = Timer.Instantiate(action.cooldownTime, 
				delegate(Timer timer){
					actionCooldownOverlayTimers[playerIndex][actionIndex] = null;
					timer.Stop();
				});
		}
		else {
			drawActionDeniedOverlay[playerIndex][actionIndex] = true;

			Timer currentActionDeniedOverlayTimer = actionDeniedOverlayTimers[playerIndex][actionIndex];
			if(currentActionDeniedOverlayTimer != null){
				currentActionDeniedOverlayTimer.Stop();
			}

			actionDeniedOverlayTimers[playerIndex][actionIndex] = Timer.Instantiate(deniedActionIconOverlayTime,
				delegate(Timer timer) {
					drawActionDeniedOverlay[playerIndex][actionIndex] = false;
					timer.Stop();
				});
		}
	}

	private Rect calculateActionIconRectangle(int playerIndex, int actionIndex){
		Player player = players[playerIndex];
		float iconSize = player.actionList[actionIndex].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
		return new Rect(iconXOffset, ACTION_TOP_OFFSET + iconSize * actionIndex, iconSize, iconSize);
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

    		// draw selected icon overlay
    		GUI.DrawTexture(playersActionIconRectangles[i][selectedActionIndices[i]], selectedActionIconOverlay);
    	}
    }

    private void DrawPlayerActions(int playerIndex){
        for (int i = 0; i < players[playerIndex].actionList.Count; i++){
        	DrawPlayerAction(playerIndex, i);

        	if(actionCooldownOverlayTimers[playerIndex][i] != null){
        		Rect actionCooldownOverlayRectangle = CalculateCooldownOverlayRectangle(playerIndex, i);
        		GUI.DrawTexture(actionCooldownOverlayRectangle, cooldownActionIconOverlay);
        	}

        	// draw denied action overlay
        	if(drawActionDeniedOverlay[playerIndex][i]){
    			GUI.DrawTexture(playersActionIconRectangles[playerIndex][i], deniedActionIconOverlay);
    		}
        }
    }

    private Rect CalculateCooldownOverlayRectangle(int playerIndex, int actionIndex){
    	Player player = players[playerIndex];
    	Action action = player.actionList[actionIndex];
    	Rect actionIconRectangle = playersActionIconRectangles[playerIndex][actionIndex];
    	Timer actionCooldownOverlayTimer = actionCooldownOverlayTimers[playerIndex][actionIndex];
    	float overlayWidth = Mathf.Clamp(
			(action.cooldownTime - actionCooldownOverlayTimer.TimeSinceLastTick) * 
				actionIconRectangle.width / 
				action.cooldownTime, 
    		0, 
    		action.cooldownTime * actionIconRectangle.width / action.cooldownTime);
    	
    	
        if (player.side == Player.Side.left){
            return new Rect(
                    actionIconRectangle.x,
                    actionIconRectangle.y,
                    overlayWidth,
                    actionIconRectangle.height);
        }
        else{
            return new Rect(
                    actionIconRectangle.x + (actionIconRectangle.width - overlayWidth),
                   	actionIconRectangle.y,
                    overlayWidth,
                    actionIconRectangle.height);
        }
    }

    private void DrawPlayerAction(int playerIndex, int actionIndex){
    	Rect actionIconRectangle = playersActionIconRectangles[playerIndex][actionIndex];
    	Action action = players[playerIndex].actionList[actionIndex];

        GUI.DrawTexture(actionIconRectangle, action.icon);

        float costXOffset = (players[playerIndex].side == Player.Side.left ? actionIconRectangle.width + 10: -50);
        GUI.Label(new Rect(
            actionIconRectangle.x + costXOffset,
            actionIconRectangle.y + actionIconRectangle.height / 2,
            50, 50), action.cost.ToString());
    }

    private void DrawCredits(){
    	for(int i = 0; i < players.Length; i++){
	    	GUI.skin.font = GameObject.FindGameObjectWithTag(Tags.style).GetComponent<Style>().font;
	    	Player player = players[i];
	    	float iconSize = player.actionList[0].icon.width;
			float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
	    	Rect creditRect = new Rect(iconXOffset, CREDITS_TOP_OFFSET, iconSize, 30);
	        GUI.Label(creditRect, player.credits.ToString());

	        Rect r;
	        if (player.side == Player.Side.left) {
	        	r = new Rect(creditRect.xMax, creditRect.height + creditsIcon.height, creditsIcon.width, creditsIcon.height);
	        }
	        else {
	        	r = new Rect(creditRect.xMin - creditsIcon.width, creditRect.height + creditsIcon.height, creditsIcon.width, creditsIcon.height);
	        }
	        
	        GUI.DrawTexture(r, creditsIcon);
	    }
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
