using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
	
	public Texture2D message;

	private bool paused;
	private GameController gameController;

	public bool Paused {
		get { return paused; }
	}

	public delegate void PauseHandler();
	public delegate void ResumeHandler();
	public event PauseHandler Pause = delegate() {};
	public event ResumeHandler Resume = delegate() {};

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		gameController.GameEnd += OnGameEnd;
	}

	private void OnGameEnd(bool win){
		enabled = false;
	}
	
	private void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			paused = !paused;

			if(paused){
				Pause();
			}
			else {
				Resume();
			}
		}

		Time.timeScale = paused ? 0 : 1;
	}
	
	private void OnGUI(){
		if(paused) showPauseDialog();
	}
	
	private void showPauseDialog(){
        GUI.DrawTexture(new Rect((Screen.width-message.width)/2,200,message.width,message.height),message);

        if(Input.GetKeyDown(KeyCode.Q)){
			Application.LoadLevel(0);	
		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel(1);	
		}

    }

    public void PauseTime(){
    	Time.timeScale = 0f;
    }

    public void ResumeTime(){
    	Time.timeScale = 1f;
    }
}
