using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class SelectActionView : MonoBehaviour
    {
        public Texture selectedActionIconOverlay;

        private UnityInputInterface unityInputInterface;
        private ActionButtonInterface actionButtonInterface;
        private Player[] players;

        public event System.Action<Player, float> ActionSelectInput = (player, selectDirection) => { };

        private void Awake()
        {
            players = Player.GetAll();
            unityInputInterface = UnityInputInterface.Get();
            actionButtonInterface = ActionButtonInterface.Get();

            unityInputInterface.PlayerActionSelectInput += OnPlayerActionSelectInput;
        }

        private void OnPlayerActionSelectInput(Player player, float selectDirection)
        {
            ActionSelectInput(player, selectDirection);
        }

        private void OnGUI()
        {
            foreach (Player player in players)
            {
                Rect drawRectangle = actionButtonInterface.GetButtonRectangleFromPlayerAndAction(player, player.SelectedAction);
                ResolutionIndependentRenderer.DrawTexture(drawRectangle, selectedActionIconOverlay);
            }
        }

        public static SelectActionView Get()
        {
            return GameObject.FindGameObjectWithTag(Tags.view).GetComponent<SelectActionView>();
        }
    }
}
