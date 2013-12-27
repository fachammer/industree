using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionInvoker : MonoBehaviour {

	public Action[] actions;

	private Dictionary<Player, Dictionary<Action, Timer>> actionCooldownTimersDictionary;

	private void Awake(){
		Player[] players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		actionCooldownTimersDictionary = new Dictionary<Player, Dictionary<Action, Timer>>();

		foreach(var player in players){
			actionCooldownTimersDictionary[player] = new Dictionary<Action, Timer>();

			foreach(var action in player.Actions){
				actionCooldownTimersDictionary[player][action] = null;
			}
		}
	}

	public bool Invoke(Player player, Action action, float actionDirection){

		if(actionCooldownTimersDictionary[player][action] != null){
			return false;
		}

		actionCooldownTimersDictionary[player][action] = Timer.AddTimer(gameObject, action.cooldownTime, delegate(Timer timer){
			timer.Stop();
			actionCooldownTimersDictionary[player][action] = null;
		});

		return action.Act(player, actionDirection);
	}
}
