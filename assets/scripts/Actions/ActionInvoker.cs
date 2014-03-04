using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionInvoker : MonoBehaviour {

	public Action[] actions;

    private PlayerActionInterface playerActionInterface;
	private Dictionary<Action, Timer> actionCooldownTimersDictionary;

	public delegate void PlayerActionHandler(ActionInvoker invoker, Action action);
    public event PlayerActionHandler ActionSuccess = delegate(ActionInvoker invoker, Action action) {};
    public event PlayerActionHandler ActionFailure = delegate(ActionInvoker invoker, Action action) {};

	private void Awake(){
        playerActionInterface = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<PlayerActionInterface>();
		actionCooldownTimersDictionary = new Dictionary<Action, Timer>();

		int i = 0;
		foreach(var action in actions){
			action.Index = i++;
			actionCooldownTimersDictionary[action] = null;
		}

        playerActionInterface.PlayerAction += OnPlayerAction;
	}

	private void OnPlayerAction(Player player, Action action, float actionDirection){
		if(player.ActionInvoker == this){
	        Invoke(player, action, actionDirection);
	    }
    }

	private void Invoke(Player player, Action action, float actionDirection){
		bool isActionSuccessful = 
			!isActionCoolingDown(action) && 
			player.Credits >= action.cost && 
			action.IsInvokable(player, actionDirection);

		if(isActionSuccessful){
			action.Invoke(player, actionDirection);
			setNewCooldownTimer(action);
			ActionSuccess(this, action);
		}
		else {
			ActionFailure(this, action);
		}
	}

	private bool isActionCoolingDown(Action action){
		return actionCooldownTimersDictionary[action] != null;
	}

	private void setNewCooldownTimer(Action action){
		actionCooldownTimersDictionary[action] = Timer.AddTimerToGameObject(gameObject, action.cooldown, delegate(Timer timer){
			timer.Stop();
			actionCooldownTimersDictionary[action] = null;
		});
	}
}
