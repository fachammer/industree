using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject timer;

	private bool gameStarted = false;
	private bool gameEnded = false;
	private Planet planet;
	private TimeManager timeManager;
	private InputManager inputManager;

	public bool GameStarted {
		get { return gameStarted; }
	}

	public bool GameEnded {
		get { return gameEnded; }
	}

	public delegate void GameEndHandler(bool win);
	public delegate void GamePauseHandler();
	public delegate void GameResumeHandler();

	public event GameEndHandler GameEnd = delegate(bool win) {};
	public event GamePauseHandler GamePause = delegate() {};
	public event GameResumeHandler GameResume = delegate() {};

	private void Awake(){
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		timeManager = GameObject.FindGameObjectWithTag(Tags.timeManager).GetComponent<TimeManager>();
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();

		planet.gameObject.GetComponent<Pollutable>().Pollute += OnPollution;
		inputManager.GamePauseInput += OnGamePauseInput;
		inputManager.GameExitInput += OnGameExitInput;
		inputManager.GameReloadInput += OnGameReloadInput;
		GameEnd += OnGameEnd;
	}

	private void OnPollution(Pollutable pollutable, int pollution){

		if (pollutable.currentPollution == planet.air)
		{           
			GameEnd(false);
		}
        else if (pollutable.currentPollution == 0)
		{    
			GameEnd(true); 
		}
	}

	private void OnGamePauseInput(){
		if(timeManager.Paused){
			timeManager.ResumeTime();
			GameResume();
		}
		else {
			timeManager.PauseTime();
			GamePause();
		}
	}

	private void OnGameExitInput(){
		if(timeManager.Paused){
			Application.LoadLevel(0);
		}
	}

	private void OnGameReloadInput(){
		if(timeManager.Paused){
			Application.LoadLevel(1);
		}
	}

	private void OnGameEnd(bool win){
		gameEnded = true;
		timeManager.PauseTime();
	}

	public void StartGame(){
		gameStarted = true;
	}
}
