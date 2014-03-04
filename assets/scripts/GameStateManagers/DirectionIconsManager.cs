using UnityEngine;
using System.Collections.Generic;

public class DirectionIconsManager : MonoBehaviour {

    public Rect directionButtonSize;
    public int selectedDirectionRectangleTopOffset;

    private Dictionary<Player, Rect> directionSlots;
    private Player[] players;

    public Dictionary<Player, Rect> DirectionSlots { get { return directionSlots; } }

    private void Awake()
    {
        players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        directionSlots = new Dictionary<Player, Rect>();

        foreach (Player player in players)
        {
            float iconXOffset = player.side == Player.Side.left ? 0 : Screen.width - directionButtonSize.width;
            directionSlots[player] = new Rect(iconXOffset, selectedDirectionRectangleTopOffset, directionButtonSize.width, directionButtonSize.height);
        }
    }
}
