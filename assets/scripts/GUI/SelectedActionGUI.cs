using UnityEngine;

public class SelectedActionGUI : MonoBehaviour {

	public Texture2D selectedActionIconOverlay;

	private ActionIconsGUI actionIconsGui;
	private SelectedActionManager selectedActionManager;

	private void Awake(){		
		actionIconsGui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionIconsGUI>();
		selectedActionManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<SelectedActionManager>();
	}

	private void OnGUI(){
		foreach(var selectedActionEntry in selectedActionManager.SelectedActionDictionary){
			Player player = selectedActionEntry.Key;
			Action action = selectedActionEntry.Value;
			GUI.DrawTexture(actionIconsGui.ActionSlots[player][action], selectedActionIconOverlay);
		}
	}
}
