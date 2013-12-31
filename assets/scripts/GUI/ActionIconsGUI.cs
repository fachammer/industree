using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionIconsGUI : MonoBehaviour {

	private Player[] players;
    private Dictionary<Player, Dictionary<Action, Rect>> actionSlots;
	private const float ACTION_TOP_OFFSET = 100;

    public Dictionary<Player, Dictionary<Action, Rect>> ActionSlots { get { return actionSlots; } }

    private const int GUI_DEPTH = 2;

	private void Awake(){
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag(Tags.gameController);
		players = gameControllerObject.GetComponent<GameController>().players;

        actionSlots = new Dictionary<Player, Dictionary<Action, Rect>>();

        foreach(Player player in players){
            actionSlots[player] = new Dictionary<Action, Rect>();

            foreach(Action action in player.Actions){
                actionSlots[player][action] = calculateActionIconRectangle(player, action);
            }
        }
	}

	private Rect calculateActionIconRectangle(Player player, Action action){
		float iconXOffset = (player.side == Player.Side.left) ? 
            0 : 
            (Screen.width - action.icon.width);
		return new Rect(iconXOffset, ACTION_TOP_OFFSET + action.icon.width * action.Index, action.icon.width, action.icon.height);
	}

	private void OnGUI(){
        GUI.depth = GUI_DEPTH;
        foreach(Player player in players){
            foreach(Action action in player.Actions){
                DrawPlayerAction(player, action);
            }
        }
	}

    private void DrawPlayerAction(Player player, Action action){
    	Rect actionIconRectangle = actionSlots[player][action];

        GUI.DrawTexture(actionIconRectangle, action.icon);

        float costXOffset = (player.side == Player.Side.left ? actionIconRectangle.width + 10: -50);
        GUI.Label(new Rect(
            actionIconRectangle.x + costXOffset,
            actionIconRectangle.y + actionIconRectangle.height / 2,
            50, 50), action.cost.ToString());
    }
}
