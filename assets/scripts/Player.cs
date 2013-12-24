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
    public List<Action> actionList;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();

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
        
        if(credits >= action.cost && action.Act(this, actionDirection)){
            credits -= action.cost;
            return true;
        }

        return false;
    }
}
