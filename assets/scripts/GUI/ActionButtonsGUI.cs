using UnityEngine;
using System.Collections.Generic;

public class ActionButtonsGUI : MonoBehaviour {

    public GameObject actionButtonTemplate;

    private ActionIconsManager actionIconsManager;
    private Dictionary<Player, Dictionary<Action, Button>> actionButtons;

    public Dictionary<Player, Dictionary<Action, Button>> ActionButtons { get { return actionButtons; } }

    private void Awake()
    {
        actionIconsManager = GameObject.FindGameObjectWithTag(Tags.gameStateManager).GetComponent<ActionIconsManager>();
        actionButtons = new Dictionary<Player, Dictionary<Action, Button>>();

        foreach (var playerEntry in actionIconsManager.ActionSlots)
        {
            Player player = playerEntry.Key;
            actionButtons[player] = new Dictionary<Action, Button>();
            foreach (var actionEntry in actionIconsManager.ActionSlots[player])
            {
                Action action = actionEntry.Key;
                Rect actionRectangle = actionEntry.Value;

                GameObject actionButton = (GameObject) Instantiate(actionButtonTemplate);
                actionButton.transform.parent = GameObject.FindGameObjectWithTag(Tags.gui).transform;
                actionButton.GetComponent<Button>().boundingRectangle = actionRectangle;

                actionButtons[player][action] = actionButton.GetComponent<Button>();
            }
        }
    }
}
