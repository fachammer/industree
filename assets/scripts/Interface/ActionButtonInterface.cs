using assets.scripts.Inspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonInterface : MonoBehaviour
{
    public GameObject templateButton;
    public MultidimensionalRectangle[] buttonRectangles;

    private Player[] players;

    private Dictionary<Player, Dictionary<Action, Button>> actionButtons;

    public event System.Action<Player, Action> ActionButtonDown = (player, action) => { };

    private void Awake()
    {
        players = Player.GetAll();

        actionButtons = new Dictionary<Player, Dictionary<Action, Button>>();

        foreach (Player player in players)
        {
            actionButtons[player] = new Dictionary<Action, Button>();
            foreach (Action action in player.actions)
            {
                GameObject actionButton = CreateActionButton(player, action);
                actionButton.GetComponent<Button>().ButtonDown += (button) => ActionButtonDown(player, action);
                actionButtons[player][action] = actionButton.GetComponent<Button>();
            }
        }
    }

    private GameObject CreateActionButton(Player player, Action action)
    {
        GameObject button = (GameObject) Instantiate(templateButton);
        button.transform.parent = gameObject.transform;

        return button;
    }

    private void Update()
    {
        SetActionButtonRectangles();
    }

    private void SetActionButtonRectangles()
    {
        foreach (Player player in players)
        {
            foreach (Action action in player.actions)
            {
                actionButtons[player][action].boundingRectangle = buttonRectangles[player.index][action.index];
            }
        }
    }

    public Rect GetButtonRectangleFromPlayerAndAction(Player player, Action action)
    {
        return actionButtons[player][action].boundingRectangle;
    }

    public static ActionButtonInterface Get()
    {
        return GameObject.FindGameObjectWithTag(Tags.userInterface).GetComponent<ActionButtonInterface>();
    }
}
