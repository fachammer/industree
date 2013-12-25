using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;
	public Texture2D cooldownActionIconOverlay;

	private InputManager inputManager;
	private Player[] players;

    private Rect[][] actionSlots;
    private bool[][] drawActionDeniedOverlay;
    private Timer[][] actionDeniedOverlayTimers;
    private Timer[][] actionCooldownOverlayTimers;

	private const float ACTION_TOP_OFFSET = 100;

    public Rect[][] ActionSlots { get { return actionSlots; } }

	private void Awake(){
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();
		inputManager.PlayerActionInput += OnPlayerActionInput;

        actionSlots = new Rect[players.Length][];

        for(int i = 0; i < actionSlots.Length; i++){
        	actionSlots[i] = new Rect[players[i].actionList.Count];
        	for(int j = 0; j < actionSlots[i].Length; j++){
        		actionSlots[i][j] = calculateActionIconRectangle(i, j);
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

	private void OnPlayerActionInput(int playerIndex, float actionDirection){
		Player player = players[playerIndex];
		int actionIndex = 0;
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
		DrawActions();
	}

    private void DrawActions(){
    	for(int i = 0; i < players.Length; i++){
    		DrawPlayerActions(i);
    	}
    }

    private void DrawPlayerActions(int playerIndex){
        for (int i = 0; i < players[playerIndex].actionList.Count; i++){
        	DrawPlayerAction(playerIndex, i);

        	// draw cooldown overlay
        	if(actionCooldownOverlayTimers[playerIndex][i] != null){
        		Rect actionCooldownOverlayRectangle = CalculateCooldownOverlayRectangle(playerIndex, i);
        		GUI.DrawTexture(actionCooldownOverlayRectangle, cooldownActionIconOverlay);
        	}

        	// draw denied action overlay
        	if(drawActionDeniedOverlay[playerIndex][i]){
    			GUI.DrawTexture(actionSlots[playerIndex][i], deniedActionIconOverlay);
    		}
        }
    }

    private Rect CalculateCooldownOverlayRectangle(int playerIndex, int actionIndex){
    	Player player = players[playerIndex];
    	Action action = player.actionList[actionIndex];
    	Rect actionIconRectangle = actionSlots[playerIndex][actionIndex];
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
    	Rect actionIconRectangle = actionSlots[playerIndex][actionIndex];
    	Action action = players[playerIndex].actionList[actionIndex];

        GUI.DrawTexture(actionIconRectangle, action.icon);

        float costXOffset = (players[playerIndex].side == Player.Side.left ? actionIconRectangle.width + 10: -50);
        GUI.Label(new Rect(
            actionIconRectangle.x + costXOffset,
            actionIconRectangle.y + actionIconRectangle.height / 2,
            50, 50), action.cost.ToString());
    }
}
