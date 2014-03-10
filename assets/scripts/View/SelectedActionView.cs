using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    [RequireComponent(typeof(Player))]
    public class SelectedActionView : View<SelectedActionViewData>
    {
        private PlayerInput playerInputInterface;
        private Player player;

        public event System.Action<Player, float> ActionSelectInput = (player, selectDirection) => { };

        private void Awake()
        {
            player = GetComponent<Player>();
            playerInputInterface = GetComponent<PlayerInput>();

            playerInputInterface.PlayerActionSelectInput += OnPlayerActionSelectInput;
        }

        private void OnPlayerActionSelectInput(Player player, float selectDirection)
        {
            ActionSelectInput(player, selectDirection);
        }

        protected override void Draw()
        {
            Rect drawRectangle = player.SelectedAction.GetComponent<ActionView>().data.bounds;
            ResolutionIndependentRenderer.DrawTexture(drawRectangle, data.selectedActionIconOverlay);
        }

        public static SelectedActionView[] GetAll()
        {
            return GameObject.FindObjectsOfType<SelectedActionView>();
        }
    }
}
