using UnityEngine;
using System.Collections;
using assets.scripts.Miscellaneous;

public class WinLoseGUI : MonoBehaviour {

	public Texture2D winDialog;
	public Texture2D loseDialog;

	private GameController gameController;

	private void Awake(){
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
	}
	
	private void OnGUI(){
		if(gameController.GameEnded){
        	if(gameController.GameWon){
        		Utilities.DrawScreenCenteredTexture(winDialog);
        	}
        	else {
        		Utilities.DrawScreenCenteredTexture(loseDialog);
        	}
        }
	}
}
