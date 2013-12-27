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

    public delegate void PlayerActionHandler(Player player, Action action, bool actionSuccessful);
    public event PlayerActionHandler PlayerAction = delegate(Player player, Action action, bool actionSuccessful) {};

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
        bool actionSuccessful = false;

        if(credits >= action.cost && actionInvoker.Invoke(this, action, actionDirection)){
            credits -= action.cost;
            actionSuccessful = true;
        }

        PlayerAction(this, action, actionSuccessful);
        return actionSuccessful;
    }
}
