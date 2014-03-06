using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionDeniedGUI : MonoBehaviour {

	public Texture2D deniedActionIconOverlay;
	public float deniedActionIconOverlayTime;

	private Player[] players;
    private Dictionary<Player, Dictionary<Action, Timer>> actionDeniedOverlayTimerDictionary;
    private ActionButtonInterface actionButtonInterface;

    private const int GUI_DEPTH = 0;

    private void Awake(){
        actionDeniedOverlayTimerDictionary = new Dictionary<Player, Dictionary<Action, Timer>>();
    	players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        actionButtonInterface = ActionButtonInterface.Get();

    	foreach(Player player in players){
            player.PlayerActionFailure += OnPlayerActionFailure;

            actionDeniedOverlayTimerDictionary[player] = new Dictionary<Action, Timer>();

            foreach(Action action in player.actions){
                actionDeniedOverlayTimerDictionary[player][action] = null;
            }
    	}
    }

    private void OnPlayerActionFailure(Player player, Action action){
        if(actionDeniedOverlayTimerDictionary[player][action] != null){
            actionDeniedOverlayTimerDictionary[player][action].Stop();
        }

        actionDeniedOverlayTimerDictionary[player][action] = Timer.AddTimerToGameObject(gameObject, deniedActionIconOverlayTime,
            delegate(Timer timer) {
                timer.Stop();
                actionDeniedOverlayTimerDictionary[player][action] = null;
            });
    }

    private void OnGUI(){
        GUI.depth = GUI_DEPTH;
        foreach(var playerEntry in actionDeniedOverlayTimerDictionary){
            Player player = playerEntry.Key;
            foreach(var actionEntry in playerEntry.Value){
                Action action = actionEntry.Key;
                if(actionEntry.Value != null){
                    GUI.DrawTexture(actionButtonInterface.GetButtonRectangleFromPlayerAndAction(player, action), deniedActionIconOverlay);
                }
            }
        }
    }
}
