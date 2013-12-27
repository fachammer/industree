using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public enum Side { left, right };
    
    public int credits;
    public int creditsUpInterval;
    public int creditsPerInterval;
    public Side side;

    private int index;
    private ActionInvoker actionInvoker;

    public int Index {
        get { return index; }
        set { index = value; }
    }

    public Action[] Actions { get { return GetComponent<ActionInvoker>().actions; } }

    public delegate void PlayerActionHandler(Player player, Action action);
    public event PlayerActionHandler PlayerActionSuccessful = delegate(Player player, Action action) {};
    public event PlayerActionHandler PlayerActionFail = delegate(Player player, Action action) {};

    private void Awake(){
        actionInvoker = gameObject.GetComponent<ActionInvoker>();

        for(int i = 0; i < actionInvoker.actions.Length; i++){
            actionInvoker.actions[i].Index = i;
        }

        Timer.AddTimer(gameObject, creditsUpInterval, OnCreditsUpTimerTick);
    }

    private void OnCreditsUpTimerTick(Timer timer){
        credits += creditsPerInterval;
    }

    public bool ActIfPossible(Action action, float actionDirection){

        if(credits >= action.cost && actionInvoker.Invoke(this, action, actionDirection)){
            credits -= action.cost;
            PlayerActionSuccessful(this, action);
            return true;
        }

        PlayerActionFail(this, action);
        return false;
    }
}
