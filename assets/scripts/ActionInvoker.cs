using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionInvoker : MonoBehaviour {

	public Action[] actions;

	private InputManager inputManager;
	private Dictionary<Player, Dictionary<Action, Timer>> actionCooldownTimersDictionary;
	private SelectedActionManager selectedActionManager;

	public delegate void PlayerActionHandler(Player player, Action action);
    public event PlayerActionHandler PlayerActionSuccess = delegate(Player player, Action action) {};
    public event PlayerActionHandler PlayerActionFail = delegate(Player player, Action action) {};

	private void Awake(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag(Tags.gameController);
		inputManager = gameControllerObject.GetComponent<InputManager>();
		Player[] players = gameControllerObject.GetComponent<GameController>().players;
		selectedActionManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<SelectedActionManager>();
		actionCooldownTimersDictionary = new Dictionary<Player, Dictionary<Action, Timer>>();

		foreach(var player in players){
			actionCooldownTimersDictionary[player] = new Dictionary<Action, Timer>();

			foreach(var action in player.Actions){
				actionCooldownTimersDictionary[player][action] = null;
			}
		}

		inputManager.PlayerActionInput += OnPlayerActionInput;
	}

	private void OnPlayerActionInput(Player player, float actionDirection){
        Action action = selectedActionManager.SelectedActionDictionary[player];
        
        Invoke(player, action, actionDirection);
    }

	private void Invoke(Player player, Action action, float actionDirection){

		bool isActionSuccessful = 
			!isPlayerActionCoolingDown(player, action) && 
			player.credits >= action.cost && 
			action.IsPerformable(player, actionDirection);

		if(isActionSuccessful){
			setNewCooldownTimer(player, action);
			player.credits -= action.cost;
			PlayerActionSuccess(player, action);
		}
		else {
			PlayerActionFail(player, action);
		}
	}

	private bool isPlayerActionCoolingDown(Player player, Action action){
		return actionCooldownTimersDictionary[player][action] != null;
	}

	private void setNewCooldownTimer(Player player, Action action){
		actionCooldownTimersDictionary[player][action] = Timer.AddTimer(gameObject, action.cooldownTime, delegate(Timer timer){
			timer.Stop();
			actionCooldownTimersDictionary[player][action] = null;
		});
	}
}
