using UnityEngine;
using System.Collections;

public class ActionCooldownGUI : MonoBehaviour {

	public Texture2D cooldownActionIconOverlay;

	private ActionIconsManager actionIconsManager;
	private ActionCooldownManager actionCooldownManager;

	private const int GUI_DEPTH = 1;

	private void Awake(){
		actionIconsManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionIconsManager>();

		actionCooldownManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionCooldownManager>();
	}

	private void OnGUI(){
		GUI.depth = GUI_DEPTH;
		foreach(var playerEntry in actionCooldownManager.ActionCooldownTimerDictionary){
			foreach(var actionEntry in playerEntry.Value){
				Timer actionCooldownTimer = actionEntry.Value;
				if(actionCooldownTimer != null){
	        		Rect actionCooldownOverlayRectangle = CalculateCooldownOverlayRectangle(playerEntry.Key, actionEntry.Key);
	        		GUI.DrawTexture(actionCooldownOverlayRectangle, cooldownActionIconOverlay);
	        	}
			}
		}
	}

	private Rect CalculateCooldownOverlayRectangle(Player player, Action action){
    	Rect actionIconRectangle = actionIconsManager.ActionSlots[player][action];
    	Timer actionCooldownOverlayTimer = actionCooldownManager.ActionCooldownTimerDictionary[player][action];
    	float overlayWidth = Mathf.Clamp(
			(action.cooldown - actionCooldownOverlayTimer.TimeSinceLastTick) * 
				actionIconRectangle.width / 
				action.cooldown, 
    		0, 
    		action.cooldown * actionIconRectangle.width / action.cooldown);
    	
    	
        if (player.side == Player.Side.left){
            return new Rect(
                    actionIconRectangle.x,
                    actionIconRectangle.y,
                    overlayWidth,
                    actionIconRectangle.height);
        }
        else{
            return new Rect(
                    actionIconRectangle.x + (actionIconRectangle.width - overlayWidth),
                   	actionIconRectangle.y,
                    overlayWidth,
                    actionIconRectangle.height);
        }
    }
}
