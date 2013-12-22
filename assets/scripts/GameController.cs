using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject timer;
	public Texture2D pauseMessage;

	private bool gameStarted = false;
	private bool gameEnded = false;
	private bool gamePaused = false;
	private Planet planet;
	private InputManager inputManager;

	public bool GameStarted {
		get { return gameStarted; }
	}

	public bool GameEnded {
		get { return gameEnded; }
	}

	public bool GamePaused {
		get { return gamePaused; }
	}

	public delegate void GameEndHandler(bool win);
	public delegate void GamePauseHandler();
	public delegate void GameResumeHandler();

	public event GameEndHandler GameEnd = delegate(bool win) {};
	public event GamePauseHandler GamePause = delegate() {};
	public event GameResumeHandler GameResume = delegate() {};

	private void Awake(){
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();

		planet.gameObject.GetComponent<Pollutable>().Pollute += OnPollution;
		inputManager.GamePauseInput += OnGamePauseInput;
		inputManager.GameExitInput += OnGameExitInput;
		inputManager.GameReloadInput += OnGameReloadInput;
	}

	private void OnGUI(){
		if(gamePaused){
			GUI.DrawTexture(new Rect((Screen.width - pauseMessage.width) / 2, 200, pauseMessage.width, pauseMessage.height), pauseMessage);
		}
	}

	private void OnPollution(Pollutable pollutable, int pollution){
		if (pollutable.currentPollution == planet.air || pollutable.currentPollution == 0)
		{       
			// call EndGame method
			EndGame();
			bool gameWin = pollutable.currentPollution == 0;
			// throw GameEnd event
			GameEnd(gameWin);
		}
	}

	private void OnGamePauseInput(){
		if(gamePaused){
			// resume game
			ResumeGame();
			// throw resume event
			GameResume();
		}
		else {
			// pause game
			PauseGame();
			// throw pause event
			GamePause();
		}
	}

	private void OnGameExitInput(){
		if(gamePaused){
			Application.LoadLevel(0);
		}
	}

	private void OnGameReloadInput(){
		if(gamePaused){
			Application.LoadLevel(1);
		}
	}

	private void EndGame(){
		gameEnded = true;
		PauseGame();
	}

	private void PauseGame(){
		gamePaused = true;
		Time.timeScale = 0;
	}

	private void ResumeGame(){
		gamePaused = false;
		Time.timeScale = 1;
	}

	public void StartGame(){
		gameStarted = true;
	}
}
