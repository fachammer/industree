using UnityEngine;
using System.Collections;

public class ActionInvoker : MonoBehaviour {

	public Action[] actions;

	private Timer[][] actionCooldownTimers;

	private void Awake(){
		Player[] players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		actionCooldownTimers = new Timer[players.Length][];

		for(int i = 0; i < actionCooldownTimers.Length; i++){
			actionCooldownTimers[i] = new Timer[actions.Length];
		}
	}

	public bool Invoke(Player player, Action action, float actionDirection){

		if(actionCooldownTimers[player.Index][action.Index] != null){
			return false;
		}

		actionCooldownTimers[player.Index][action.Index] = Timer.Instantiate(action.cooldownTime, delegate(Timer timer){
			timer.Stop();
			actionCooldownTimers[player.Index][action.Index] = null;
		});

		return action.Act(player, actionDirection);
	}
}
