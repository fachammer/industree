using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets.scripts.Controller;
using System;

public class CreditsManager : MonoBehaviour
{
    public int initialCredits;
    public int creditsUpInterval;
    public int creditsPerInterval;

    private int credits;
    private ActionInvoker actionInvoker;

    public int Credits { get { return credits; } }

    private void Awake(){
        actionInvoker = GetComponent<ActionInvoker>();
        actionInvoker.ActionSuccess += OnActionSuccess;
    }

    private void Start()
    {
        credits = initialCredits;
        Timer.Start(creditsUpInterval, OnCreditsUpTimerTick);
    }

    private void OnActionSuccess(Player player, Action action, float actionDirection)
    {
        credits -= action.cost;
    }

    private void OnCreditsUpTimerTick(Timer timer){
        credits += creditsPerInterval;
    }

    public void IncreaseCredits(int creditsAmount){
        credits += creditsAmount;
    }
}
