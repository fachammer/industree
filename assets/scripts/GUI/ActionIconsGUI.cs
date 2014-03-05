using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionIconsGUI : MonoBehaviour {

    private ActionIconsManager actionIconsManager;

    private const int GUI_DEPTH = 2;

    private void Awake()
    {
        actionIconsManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionIconsManager>();
    }

	private void OnGUI(){
        GUI.depth = GUI_DEPTH;
        foreach(Player player in actionIconsManager.ActionSlots.Keys){
            foreach(Action action in player.actions){
                DrawPlayerAction(player, action);
            }
        }
	}

    private void DrawPlayerAction(Player player, Action action){
    	Rect actionIconRectangle = actionIconsManager.ActionSlots[player][action];

        GUI.DrawTexture(actionIconRectangle, action.icon);

        float costXOffset = (player.side == Player.Side.left ? actionIconRectangle.width + 10: -50);
        GUI.Label(new Rect(
            actionIconRectangle.x + costXOffset,
            actionIconRectangle.y + actionIconRectangle.height / 2,
            50, 50), action.cost.ToString());
    }
}
