using UnityEngine;

public class SelectedActionGUI : MonoBehaviour {

	public Texture2D selectedActionIconOverlay;

	private ActionsGUI actionsGui;
	private SelectedActionManager selectedActionManager;

	private void Awake(){		
		actionsGui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionsGUI>();
		selectedActionManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<SelectedActionManager>();
	}

	private void OnGUI(){
		foreach(var selectedActionEntry in selectedActionManager.SelectedActionDictionary){
			Player player = selectedActionEntry.Key;
			Action action = selectedActionEntry.Value;
			GUI.DrawTexture(actionsGui.ActionSlots[player][action], selectedActionIconOverlay);
		}
	}
}
