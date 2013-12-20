using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject timer;

	public bool gameStarted = false;
	public bool gameEnded = false;

	public delegate void GameEndHandler(bool win);
	public event GameEndHandler GameEnd = delegate(bool win){};

	private Planet planet;
	private TimeManager timeManager;

	void Start(){
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		timeManager = GameObject.FindGameObjectWithTag(Tags.timeManager).GetComponent<TimeManager>();
		planet.pollutable.Pollute += OnPollution;
		GameEnd += OnGameEnd;
	}

	void OnPollution(Pollutable pollutable, int pollution){

		if (pollutable.currentPollution == planet.air)
		{           
			GameEnd(false);
		}
        else if (pollutable.currentPollution == 0)
		{    
			GameEnd(true); 
		}
	}

	void OnGameEnd(bool win){
		gameEnded = true;
		timeManager.PauseTime();
	}
}
