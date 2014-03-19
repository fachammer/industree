using System;
using System.Collections.Generic;
using UnityEngine;
using Industree.Rendering;
using Industree.Facade;
using Industree.Facade.Internal;

namespace Industree.View
{
    [RequireComponent(typeof(IPlayer))]
    public class SelectedActionView : View<SelectedActionViewData>
    {
        private PlayerInput playerInputInterface;
        private IPlayer player;

        public event System.Action<IPlayer, float> ActionSelectInput = (player, selectDirection) => { };

        private void Awake()
        {
            player = GetComponent<Player>();
            playerInputInterface = GetComponent<PlayerInput>();

            playerInputInterface.PlayerActionSelectInput += OnPlayerActionSelectInput;
        }

        private void OnPlayerActionSelectInput(IPlayer player, float selectDirection)
        {
            ActionSelectInput(player, selectDirection);
        }

        protected override void Draw()
        {
            Rect drawRectangle = player.SelectedAction.IconBounds;
            ResolutionIndependentRenderer.DrawTexture(drawRectangle, data.guiSkin.button.focused.background);
        }

        public static SelectedActionView[] GetAll()
        {
            return GameObject.FindObjectsOfType<SelectedActionView>();
        }
    }
}
