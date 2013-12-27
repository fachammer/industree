using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionDeniedGUI : MonoBehaviour {

	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;

	private Player[] players;
    private Dictionary<Player, Dictionary<Action, Timer>> actionDeniedOverlayTimers;
	private ActionsGUI actionsGui;

    private void Awake(){
        actionDeniedOverlayTimers = new Dictionary<Player, Dictionary<Action, Timer>>();
    	players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
    	actionsGui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionsGUI>();

    	foreach(Player player in players){
            player.PlayerActionFail += OnPlayerActionFail;

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
        foreach(var playerEntry in actionDeniedOverlayTimers){
            Player player = playerEntry.Key;
            foreach(var actionEntry in playerEntry.Value){
                Action action = actionEntry.Key;
                if(actionDeniedOverlayTimers[player][action] != null){
                    GUI.DrawTexture(actionsGui.ActionSlots[player][action], deniedActionIconOverlay);
                }
            }
        }
    }
}
