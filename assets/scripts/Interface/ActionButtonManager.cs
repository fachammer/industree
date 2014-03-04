using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonManager : MonoBehaviour
{
    private Player[] players;

    private Dictionary<Player, Dictionary<Action, Button>> actionButtons;

    private void Awake()
    {
        players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
        actionButtons = new Dictionary<Player, Dictionary<Action, Button>>();

        foreach (Player player in players)
        {
            actionButtons[player] = new Dictionary<Action,Button>();
            foreach (Action action in player.Actions)
            {
                GameObject actionButton = CreateActionButton(player, action);
                actionButtons[player][action] = actionButton.GetComponent<Button>();
            }
        }
    }

    private GameObject CreateActionButton(Player player, Action action)
    {
        // Create action button
        throw new NotImplementedException();
    }

    public Button GetButtonFromPlayerAndAction(Player player, Action action)
    {
        return actionButtons[player][action];
    }
}
