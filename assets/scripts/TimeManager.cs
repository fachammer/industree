using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
	
	public bool paused = false;

	public delegate void PauseHandler();
	public delegate void ResumeHandler();
	public event PauseHandler Pause = delegate() {};
	public event ResumeHandler Resume = delegate() {};
	
	public Texture2D message;
	
	void Update () {
		if(GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>().gameEnded) return;

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
	
	void OnGUI(){
		if(paused) showPauseDialog();
	}
	
	void showPauseDialog(){
        GUI.DrawTexture(new Rect((Screen.width-message.width)/2,200,message.width,message.height),message);

        if(Input.GetKeyDown(KeyCode.Q)){
			Application.LoadLevel(0);	
		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel(1);	
		}

    }
}
