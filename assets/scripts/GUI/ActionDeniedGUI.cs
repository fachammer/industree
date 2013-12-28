using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionDeniedGUI : MonoBehaviour {

	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;

	private Player[] players;
    private Dictionary<Player, Dictionary<Action, Timer>> actionDeniedOverlayTimers;
	private ActionIconsGUI actionIconsGui;

    private const int GUI_DEPTH = 0;

    private void Awake(){
        actionDeniedOverlayTimers = new Dictionary<Player, Dictionary<Action, Timer>>();
    	players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
    	actionIconsGui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionIconsGUI>();

    	foreach(Player player in players){
            player.ActionInvoker.PlayerActionFail += OnPlayerActionFail;

            actionDeniedOverlayTimers[player] = new Dictionary<Action, Timer>();

            foreach(Action action in player.Actions){
                actionDeniedOverlayTimers[player][action] = null;
            }
    	}
    }

    private void OnPlayerActionFail(Player player, Action action){
        if(actionDeniedOverlayTimers[player][action] != null){
            actionDeniedOverlayTimers[player][action].Stop();
            actionDeniedOverlayTimers[player][action] = null;
        }

        actionDeniedOverlayTimers[player][action] = Timer.AddTimer(gameObject, deniedActionIconOverlayTime,
            delegate(Timer timer) {
                timer.Stop();
                actionDeniedOverlayTimers[player][action] = null;
            });
    }

    private void OnGUI(){
        GUI.depth = GUI_DEPTH;
        foreach(var playerEntry in actionDeniedOverlayTimers){
            Player player = playerEntry.Key;
            foreach(var actionEntry in playerEntry.Value){
                Action action = actionEntry.Key;
                if(actionDeniedOverlayTimers[player][action] != null){
                    GUI.DrawTexture(actionIconsGui.ActionSlots[player][action], deniedActionIconOverlay);
                }
            }
        }
    }
}
