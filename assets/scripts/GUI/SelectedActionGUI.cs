using UnityEngine;

public class SelectedActionGUI : MonoBehaviour {

	public Texture2D selectedActionIconOverlay;

	private ActionIconsManager actionIconsManager;
	private SelectedActionManager selectedActionManager;

	private void Awake(){		
		actionIconsManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionIconsManager>();
		selectedActionManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<SelectedActionManager>();
	}

	private void OnGUI(){
		foreach(var selectedActionEntry in selectedActionManager.SelectedActionDictionary){
			Player player = selectedActionEntry.Key;
			Action action = selectedActionEntry.Value;
			GUI.DrawTexture(actionIconsManager.ActionSlots[player][action], selectedActionIconOverlay);
		}
	}
}
