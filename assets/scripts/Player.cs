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

    public ActionInvoker ActionInvoker { get { return GetComponent<ActionInvoker>(); } }

    public Action[] Actions { get { return ActionInvoker.actions; } }

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
}
