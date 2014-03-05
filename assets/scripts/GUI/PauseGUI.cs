using UnityEngine;
using System.Collections;
using assets.scripts.Miscellaneous;

public class PauseGUI : MonoBehaviour {

	public Texture2D pauseDialog;

	private GameController gameController;

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
	}

	private void OnGUI(){
		if(gameController.GamePaused && !gameController.GameEnded){
			Utilities.DrawScreenCenteredTexture(pauseDialog);
		}
	}
}
