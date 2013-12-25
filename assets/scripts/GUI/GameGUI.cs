using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	private Player[] players;
    private Action[] actions;
    private InputManager inputManager;

    private Rect[][] actionSlots;

	private const float ACTION_TOP_OFFSET = 100;

    public Rect[][] ActionSlots { get { return actionSlots; } }

	private void Awake(){
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        actions = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>().actions;
        inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();

        actionSlots = new Rect[players.Length][];
        for(int i = 0; i < actionSlots.Length; i++){
        	actionSlots[i] = new Rect[actions.Length];
        	for(int j = 0; j < actionSlots[i].Length; j++){
        		actionSlots[i][j] = calculateActionIconRectangle(i, j);
        	}
        }

        inputManager.PlayerActionInput += OnPlayerActionInput;
	}

	private void OnPlayerActionInput(int playerIndex, float actionDirection){
        Player player = players[playerIndex];   
        int actionIndex = GetComponent<CurrentlySelectedActionGUI>().SelectedActionIndices[playerIndex];
        Action action = actions[actionIndex];
        
        player.ActIfPossible(action, actionDirection);
    }

	private Rect calculateActionIconRectangle(int playerIndex, int actionIndex){
		Player player = players[playerIndex];
		float iconSize = actions[actionIndex].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
		return new Rect(iconXOffset, ACTION_TOP_OFFSET + iconSize * actionIndex, iconSize, iconSize);
	}

	private void OnGUI(){
		DrawActions();
	}

    private void DrawActions(){
    	for(int i = 0; i < players.Length; i++){
    		DrawPlayerActions(i);
    	}
    }

    private void DrawPlayerActions(int playerIndex){
        for (int i = 0; i < actions.Length; i++){
        	DrawPlayerAction(playerIndex, i);
        }
    }

    private void DrawPlayerAction(int playerIndex, int actionIndex){
    	Rect actionIconRectangle = actionSlots[playerIndex][actionIndex];
    	Action action = actions[actionIndex];

        GUI.DrawTexture(actionIconRectangle, action.icon);

        float costXOffset = (players[playerIndex].side == Player.Side.left ? actionIconRectangle.width + 10: -50);
        GUI.Label(new Rect(
            actionIconRectangle.x + costXOffset,
            actionIconRectangle.y + actionIconRectangle.height / 2,
            50, 50), action.cost.ToString());
    }
}
