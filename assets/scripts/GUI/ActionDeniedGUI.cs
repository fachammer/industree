using UnityEngine;
using System.Collections;

public class ActionDeniedGUI : MonoBehaviour {

	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;

	private Player[] players;
	private GameGUI gui;
	private Action[] actions;

	private bool[][] drawActionDeniedOverlay;
    private Timer[][] actionDeniedOverlayTimers;

    private void Awake(){
    	players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
    	gui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<GameGUI>();
    	actions = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>().actions;

    	foreach(Player player in players){
    		player.PlayerAction += OnPlayerAction;
    	}

    	drawActionDeniedOverlay = new bool[players.Length][];
    	actionDeniedOverlayTimers = new Timer[players.Length][];

        for(int i = 0; i < drawActionDeniedOverlay.Length; i++){
        	drawActionDeniedOverlay[i] = new bool[actions.Length];
        	actionDeniedOverlayTimers[i] = new Timer[actions.Length];
        }
    }

    private void OnPlayerAction(Player player, Action action, bool actionSuccessful){
    	if(!actionSuccessful) {
			drawActionDeniedOverlay[player.Index][action.Index] = true;

			Timer currentActionDeniedOverlayTimer = actionDeniedOverlayTimers[player.Index][action.Index];
			if(currentActionDeniedOverlayTimer != null){
				currentActionDeniedOverlayTimer.Stop();
			}

			actionDeniedOverlayTimers[player.Index][action.Index] = Timer.AddTimer(gameObject, deniedActionIconOverlayTime,
				delegate(Timer timer) {
					drawActionDeniedOverlay[player.Index][action.Index] = false;
					timer.Stop();
				});
		}
    }


    private void OnGUI(){
    	for(int i = 0; i < players.Length; i++){
    		for(int j = 0; j < actions.Length; j++){
	        	if(drawActionDeniedOverlay[i][j]){
	    			GUI.DrawTexture(gui.ActionSlots[i][j], deniedActionIconOverlay);
	    		}
    		}
    	}
    }
}
