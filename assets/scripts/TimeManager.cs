using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
	
	public Texture2D message;

	private bool paused;

	public bool Paused {
		get { return paused; }
	}

	private void OnGameEnd(bool win){
		enabled = false;
	}
	
	private void OnGUI(){
		if(paused) {
			GUI.DrawTexture(new Rect((Screen.width-message.width)/2,200,message.width,message.height),message);
		}
	}
	
    public void PauseTime(){
    	paused = true;
    	Time.timeScale = 0f;
    }

    public void ResumeTime(){
    	paused = false;
    	Time.timeScale = 1f;
    }
}
