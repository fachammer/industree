using UnityEngine;
using System.Collections;

public class ActionCooldownGUI : MonoBehaviour {

	public Texture2D cooldownActionIconOverlay;

	private ActionsGUI actionsGui;
	private ActionCooldownManager actionCooldownManager;

	private void Awake(){
		actionsGui = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<ActionsGUI>();

		actionCooldownManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionCooldownManager>();
	}

	private void OnGUI(){
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
    	Rect actionIconRectangle = actionsGui.ActionSlots[player][action];
    	Timer actionCooldownOverlayTimer = actionCooldownManager.ActionCooldownTimerDictionary[player][action];
    	float overlayWidth = Mathf.Clamp(
			(action.cooldownTime - actionCooldownOverlayTimer.TimeSinceLastTick) * 
				actionIconRectangle.width / 
				action.cooldownTime, 
    		0, 
    		action.cooldownTime * actionIconRectangle.width / action.cooldownTime);
    	
    	
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
