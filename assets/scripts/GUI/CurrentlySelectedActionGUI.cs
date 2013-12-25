using UnityEngine;
using System.Collections;

public class CurrentlySelectedActionGUI : MonoBehaviour {

	public Texture2D selectedActionIconOverlay;

	private InputManager inputManager;
	private Player[] players;
	private GameGUI gui;
	private int[] selectedActionIndices;

	private void Awake(){
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		gui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<GameGUI>();
		selectedActionIndices = new int[players.Length];

		inputManager.PlayerSelectInput += OnPlayerSelectInput;
	}

	private void OnPlayerSelectInput(int playerIndex, float selectDirection){
		if(selectDirection > 0){
			if(selectedActionIndices[playerIndex] > 0){
				selectedActionIndices[playerIndex]--;
			}
		}
		else {
			if(selectedActionIndices[playerIndex] < players[playerIndex].actionList.Count - 1){
				selectedActionIndices[playerIndex]++;
			}
		}
	}

	private void OnGUI(){
		for(int i = 0; i < players.Length; i++){
			GUI.DrawTexture(gui.ActionSlots[i][selectedActionIndices[i]], selectedActionIconOverlay);
		}
	}
}
