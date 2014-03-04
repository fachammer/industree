using UnityEngine;
using System.Collections.Generic;

public class ActionIconsManager : MonoBehaviour {

    private Dictionary<Player, Dictionary<Action, Rect>> actionSlots;
    private Player[] players;

    public Dictionary<Player, Dictionary<Action, Rect>> ActionSlots { get { return actionSlots; } }

    private const float ACTION_TOP_OFFSET = 100;

    private void Awake()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag(Tags.gameController);
        players = gameControllerObject.GetComponent<GameController>().players;

        actionSlots = new Dictionary<Player, Dictionary<Action, Rect>>();

        foreach (Player player in players)
        {
            actionSlots[player] = new Dictionary<Action, Rect>();

            foreach (Action action in player.Actions)
            {
                actionSlots[player][action] = calculateActionIconRectangle(player, action);
            }
        }
    }

    private Rect calculateActionIconRectangle(Player player, Action action)
    {
        float iconXOffset = (player.side == Player.Side.left) ?
            0 :
            (Screen.width - action.icon.width);
        return new Rect(iconXOffset, ACTION_TOP_OFFSET + action.icon.width * action.Index, action.icon.width, action.icon.height);
    }
}
