using UnityEngine;
using System.Collections;

public class AudioGameEndHandler : MonoBehaviour {

	public AudioClip soundVictory;
	public AudioClip soundDefeated;
   
	private void Awake () {
		// GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().GameEnd += OnGameEnd;
	}

	private void OnGameEnd(bool win){
		if(win){
			audio.PlayOneShot(soundVictory);
		}
		else {
			audio.PlayOneShot(soundDefeated);
		}
	}
}
