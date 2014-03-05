using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player[] players;

	private bool gameStarted = false;
	private bool gameEnded = false;
	private bool gamePaused = false;
	private bool gameWon = false;
	private Pollutable pollutable;
	private UnityInputInterface inputManager;

	public bool GameStarted { get { return gameStarted; } }
	public bool GameEnded { get { return gameEnded; } }
	public bool GamePaused { get { return gamePaused; } }
	public bool GameWon { get { return gameEnded && gameWon; } }
	public bool GameLost { get { return gameEnded && !gameWon; } }

	public delegate void GameEndHandler(bool win);
	public delegate void GamePauseHandler();
	public delegate void GameResumeHandler();
    public delegate void GameStartHandler();

	public event GameEndHandler GameEnd = delegate(bool win) {};
	public event GamePauseHandler GamePause = delegate() {};
	public event GameResumeHandler GameResume = delegate() {};
    public event GameStartHandler GameStart = delegate() { };

	private void Awake(){
		pollutable = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Pollutable>();
		inputManager = UnityInputInterface.Get();

		pollutable.FullPollution += OnFullPollution;
		pollutable.NoPollution += OnNoPollution;
		inputManager.GamePauseInput += OnGamePauseInput;
		inputManager.GameExitInput += OnGameExitInput;
		inputManager.GameReloadInput += OnGameReloadInput;

		Random.seed = System.Environment.TickCount;

		/*
		Screen.lockCursor = true;
		Screen.showCursor = false;
		*/
	}

	private void OnFullPollution(Pollutable pollutable){
		EndGame();
		GameEnd(false);
	}

	private void OnNoPollution(Pollutable pollutable){
		EndGame();
		GameEnd(true);
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
			ResumeGame();
			Application.LoadLevel(0);
		}
	}

	private void OnGameReloadInput(){
		if(gamePaused){
			ResumeGame();
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
        GameStart();
	}
}
