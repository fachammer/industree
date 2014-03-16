using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets.scripts.Controller;
using System;

public class Player : MonoBehaviour
{
    public int index;

    private Action[] actions;
    private Action selectedAction;

    public Action[] Actions { get { return actions; } }

    public Action SelectedAction { get { return selectedAction; } }

    private void Awake(){

        actions = transform.GetComponentsInChildren<Action>();
        Array.Sort<Action>(actions, (a, b) => a.index - b.index);

        selectedAction = actions[0];
    }

    public void SelectNextAction()
    {
        if (selectedAction.index < actions.Length - 1)
        {
            selectedAction = actions[selectedAction.index + 1];
        }
        else
        {
            selectedAction = actions[0];
        } 
    }

    public void SelectPreviousAction()
    {
        if (selectedAction.index > 0)
        {
            selectedAction = actions[selectedAction.index - 1];
        }
        else
        {
            selectedAction = actions[actions.Length - 1];
        } 
    }

    public static Player[] GetAll()
    {
        Player[] players = Array.ConvertAll(GameObject.FindGameObjectsWithTag(Tags.player), (gameObject) => gameObject.GetComponent<Player>());
        Array.Sort<Player>(players, (p1, p2) => p1.index - p2.index);
        return players;
    }
}
