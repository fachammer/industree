using UnityEngine;
using System.Collections;

public class CurrentlySelectedActionGUI : MonoBehaviour {

	public Texture2D selectedActionIconOverlay;

	private InputManager inputManager;
	private Player[] players;
	private GameGUI gui;
	private Action[] actions;
	private int[] selectedActionIndices;

	public int[] SelectedActionIndices { get { return selectedActionIndices; } }

	private void Awake(){
		inputManager = GameObject.FindGameObjectWithTag(Tags.inputManager).GetComponent<InputManager>();
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		gui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<GameGUI>();
		actions = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>().actions;

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
			if(selectedActionIndices[playerIndex] < actions.Length - 1){
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
