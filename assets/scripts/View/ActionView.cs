using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class ActionView : MonoBehaviour
    {
        private UnityInputInterface unityInputInterface;
        private ActionButtonInterface actionButtonInterface;
        private Player[] players;

        public event System.Action<Player, Action, float> ActionInput = (Player player, Action action, float actionDirection) => { };

        private void Awake()
        {
            players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;

            unityInputInterface = UnityInputInterface.Get();
            actionButtonInterface = ActionButtonInterface.Get();

            unityInputInterface.PlayerActionInput += OnPlayerActionInput;
        }

        private void OnPlayerActionInput(Player player, float actionDirection)
        {
            ActionInput(player, player.SelectedAction, actionDirection);
        }

        private void OnGUI()
        {
            foreach (Player player in players)
            {
                foreach (Action action in player.actions)
                {
                    DrawAction(player, action);
                }
            }
        }

        private void DrawAction(Player player, Action action)
        {
             ResolutionIndependentRenderer.DrawTexture(actionButtonInterface.GetButtonRectangleFromPlayerAndAction(player, action), action.icon);
        }

        public static ActionView Get()
        {
            return GameObject.FindGameObjectWithTag(Tags.view).GetComponent<ActionView>();
        }
    }
}
