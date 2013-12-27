using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedActionManager : MonoBehaviour {

	private InputManager inputManager;
	private Dictionary<Player, Action> selectedActionDictionary;
	private Dictionary<Player, int> selectedActionIndices;

	public Dictionary<Player, Action> SelectedActionDictionary { get { return selectedActionDictionary; } }

	private void Awake(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag(Tags.gameController);
		inputManager = gameControllerObject.GetComponent<InputManager>();
		Player[] players = gameControllerObject.GetComponent<GameController>().players;
		
		selectedActionDictionary = new Dictionary<Player, Action>();
		selectedActionIndices = new Dictionary<Player, int>();

		foreach(var player in players){
			selectedActionDictionary[player] = player.Actions[0];
			selectedActionIndices[player] = 0;
		}

		inputManager.PlayerSelectInput += OnPlayerSelectInput;
	}

	private void OnPlayerSelectInput(Player player, float selectDirection){
		if(selectDirection > 0){
			if(selectedActionIndices[player] > 0){
				selectedActionIndices[player]--;
				selectedActionDictionary[player] = player.Actions[selectedActionIndices[player]];
			}
		}
		else {
			if(selectedActionIndices[player] < player.Actions.Length - 1){
				selectedActionIndices[player]++;
				selectedActionDictionary[player] = player.Actions[selectedActionIndices[player]];
			}
		}
	}
}
