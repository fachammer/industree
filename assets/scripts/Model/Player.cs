using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets.scripts.Controller;
using System;

public class Player : MonoBehaviour
{
    public enum Side { left, right };
    
    public int initialCredits;
    public int creditsUpInterval;
    public int creditsPerInterval;
    public Side side;
    public Action[] actions;
    public int index;

    private int credits;
    private Action selectedAction;

    public int Credits { get { return credits; } }

    public Action SelectedAction { get { return selectedAction; } }

    public event Action<Player, Action> PlayerActionSuccess = (player, action) => { };
    public event Action<Player, Action> PlayerActionFailure = (player, action) => { };

    private void Awake(){
        credits = initialCredits;
        selectedAction = actions[0];

        Timer.Start(creditsUpInterval, OnCreditsUpTimerTick);

        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].index = i;
        }
    }

    public void SucceedAction(Action action){
        credits -= action.cost;
        PlayerActionSuccess(this, action);
    }

    public void FailAction(Action action){
        PlayerActionFailure(this, action);
    }

    public void SelectNextAction()
    {
        if(selectedAction.index < actions.Length - 1)
        {
            selectedAction = actions[selectedAction.index + 1];
        }
        else
        {
            selectedAction = actions[0];
        } 
    }

    public void SelectPreviousAction()
    {
        if (selectedAction.index > 0)
        {
            selectedAction = actions[selectedAction.index - 1];
        }
        else
        {
            selectedAction = actions[actions.Length - 1];
        } 
    }

    private void OnCreditsUpTimerTick(Timer timer){
        credits += creditsPerInterval;
    }

    public void IncreaseCredits(int creditsAmount){
        credits += creditsAmount;
    }

    public static Player[] GetAll()
    {
        Player[] players = Array.ConvertAll(GameObject.FindGameObjectsWithTag(Tags.player), (gameObject) => gameObject.GetComponent<Player>());
        Array.Sort<Player>(players, (p1, p2) => p1.index - p2.index);
        return players;
    }
}
