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
    private GameController gameController;
    private ActionInvoker actionInvoker;

    public int Index {
        get { return index; }
        set { index = value; }
    }

    public delegate void PlayerActionHandler(Player player, Action action, bool actionSuccessful);
    public event PlayerActionHandler PlayerAction = delegate(Player player, Action action, bool actionSuccessful) {};

    private void Awake(){
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        actionInvoker = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>();

        for(int i = 0; i < actionInvoker.actions.Length; i++){
            actionInvoker.actions[i].Index = i;
        }

        gameController.GamePause += OnGamePause;
        gameController.GameResume += OnGameResume;
        gameController.GameEnd += OnGameEnd;

        Timer.Instantiate(creditsUpInterval, OnCreditsUpTimerTick);
    }

    private void OnGamePause(){
        enabled = false;
    }

    private void OnGameResume(){
        enabled = true;
    }

    private void OnGameEnd(bool win){
        enabled = false;
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
