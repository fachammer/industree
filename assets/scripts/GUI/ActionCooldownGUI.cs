using UnityEngine;
using System.Collections;

public class ActionCooldownGUI : MonoBehaviour {

	public Texture2D cooldownActionIconOverlay;

	private Player[] players;
	private GameGUI gui;
	private Action[] actions;
	private Timer[][] actionCooldownOverlayTimers;

	private void Awake(){
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		gui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<GameGUI>();
		actions = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>().actions;

		actionCooldownOverlayTimers = new Timer[players.Length][];
        for(int i = 0; i < actionCooldownOverlayTimers.Length; i++){
        	actionCooldownOverlayTimers[i] = new Timer[actions.Length];
        }

        foreach(Player player in players){
        	player.PlayerAction += OnPlayerAction;
        }
	}

	private void OnPlayerAction(Player player, Action action, bool actionSuccessful){
		if(actionSuccessful){
			actionCooldownOverlayTimers[player.Index][action.Index] = Timer.AddTimer(gameObject, action.cooldownTime, 
				delegate(Timer timer){
					actionCooldownOverlayTimers[player.Index][action.Index] = null;
					timer.Stop();
				});
		}
	}

	private void OnGUI(){
		for(int i = 0; i < players.Length; i++){
			for(int j = 0; j < actions.Length; j++){
	        	if(actionCooldownOverlayTimers[i][j] != null){
	        		Rect actionCooldownOverlayRectangle = CalculateCooldownOverlayRectangle(i, j);
	        		GUI.DrawTexture(actionCooldownOverlayRectangle, cooldownActionIconOverlay);
	        	}
			}
        }
	}

	private Rect CalculateCooldownOverlayRectangle(int playerIndex, int actionIndex){
    	Player player = players[playerIndex];
    	Action action = actions[actionIndex];
    	Rect actionIconRectangle = gui.ActionSlots[playerIndex][actionIndex];
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
}
