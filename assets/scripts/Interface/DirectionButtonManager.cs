using System;
using System.Collections.Generic;
using UnityEngine;

public class DirectionButtonManager : MonoBehaviour
{
    private Player[] players;

    private Dictionary<Player, Button> directionButtons;

    private void Awake()
    {
        players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        directionButtons = new Dictionary<Player, Button>();

        foreach (Player player in players)
        {
            GameObject directionButton = CreateDirectionButton(player);
            directionButtons[player] = directionButton.GetComponent<Button>();
        }
    }

    private GameObject CreateDirectionButton(Player player)
    {
        // create direction button
        throw new NotImplementedException();
    }

    public Button GetDirectionButtonFromPlayer(Player player)
    {
        return directionButtons[player];
    }
}
