using UnityEngine;
using System.Collections;

public class PauseGUI : MonoBehaviour {

	public Texture2D pauseDialog;

	private GameController gameController;
	private Rect pauseDialogRectangle;

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		pauseDialogRectangle = new Rect(
			(Screen.width - pauseDialog.width) / 2, 
			(Screen.height - pauseDialog.height) / 2, 
			pauseDialog.width, 
			pauseDialog.height);
	}

	private void OnGUI(){
		if(gameController.GamePaused && !gameController.GameEnded){
			GUI.DrawTexture(pauseDialogRectangle, pauseDialog);
		}
	}
}
