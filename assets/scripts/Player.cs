using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public enum Side { left, right };
    
    public int initialCredits;
    public int creditsUpInterval;
    public int creditsPerInterval;
    public Side side;

    private int credits;
    private ActionInvoker actionInvoker;

    public int Credits { get { return credits; } }
    public ActionInvoker ActionInvoker { get { return GetComponent<ActionInvoker>(); } }
    public Action[] Actions { get { return ActionInvoker.actions; } }

    public delegate void PlayerActionHandler(Player player, Action action);
    public event PlayerActionHandler PlayerActionSuccess = delegate(Player player, Action action) {};
    public event PlayerActionHandler PlayerActionFailure = delegate(Player player, Action action) {};

    private void Awake(){
        credits = initialCredits;
        actionInvoker = gameObject.GetComponent<ActionInvoker>();

        actionInvoker.ActionSuccess += OnActionSuccess;
        actionInvoker.ActionFailure += OnActionFailure;

        Timer.AddTimerToGameObject(gameObject, creditsUpInterval, OnCreditsUpTimerTick);
    }

    private void OnActionSuccess(ActionInvoker invoker, Action action){
        credits -= action.cost;
        PlayerActionSuccess(this, action);
    }

    private void OnActionFailure(ActionInvoker invoker, Action action){
        PlayerActionFailure(this, action);
    }

    private void OnCreditsUpTimerTick(Timer timer){
        credits += creditsPerInterval;
    }

    public void IncreaseCredits(int creditsAmount){
        credits += creditsAmount;
    }
}
