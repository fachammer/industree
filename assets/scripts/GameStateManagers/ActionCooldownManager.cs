using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionCooldownManager : MonoBehaviour {

	private Dictionary<Player, Dictionary<Action, Timer>> actionCooldownTimerDictionary;

	public Dictionary<Player, Dictionary<Action, Timer>> ActionCooldownTimerDictionary { get { return actionCooldownTimerDictionary; } }

	private void Awake(){
		Player[] players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;

		actionCooldownTimerDictionary = new Dictionary<Player, Dictionary<Action, Timer>>();

		foreach(var player in players){
			actionCooldownTimerDictionary[player] = new Dictionary<Action, Timer>();

			foreach(var action in player.Actions){
				actionCooldownTimerDictionary[player][action] = null;
			}

			player.PlayerAction += OnPlayerAction;
		}
	}

	private void OnPlayerAction(Player player, Action action, bool actionSuccessful){
		if(actionSuccessful){
			actionCooldownTimerDictionary[player][action] = Timer.AddTimer(gameObject, action.cooldownTime, 
				delegate(Timer timer){
					actionCooldownTimerDictionary[player][action] = null;
					timer.Stop();
				});
		}
	}
}
