using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Planet : MonoBehaviour {
	
	public float air;
	public AudioClip soundVictory;
	public AudioClip soundDefeated;
	[HideInInspector]
	public Pollutable pollutable;

	private GameController gameController;
   
	private void Awake () {
		pollutable = GetComponent<Pollutable>();
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		gameController.GameEnd += OnGameEnd;
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