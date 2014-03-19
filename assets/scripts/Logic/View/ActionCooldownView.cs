using UnityEngine;
using System.Collections;
using Industree.Rendering;
using Industree.Model.Actions;
using Industree.Facade;
using Industree.Facade.Internal;

namespace Industree.View
{
    

    [RequireComponent(typeof(IAction))]
    [RequireComponent(typeof(ActionView))]
    public class ActionCooldownView : View<ActionCooldownViewData>
    {
        private IAction action;
        private ActionView actionView;

        private void Awake()
        {
            action = GetComponent<Action>();
            actionView = GetComponent<ActionView>();
        }

        protected override void Draw()
        {
            if (action.IsCoolingDown)
            {
                Rect drawRectangle = CalculateCooldownOverlayRectangle(action);
                ResolutionIndependentRenderer.DrawTexture(drawRectangle, data.cooldownOverlay);
            }
        }

        private Rect CalculateCooldownOverlayRectangle(IAction action)
        {
            Rect actionIconRectangle = actionView.data.bounds;
            float overlayWidth = Mathf.Clamp(
                action.GetRemainingCooldown() * actionIconRectangle.width / action.Cooldown,
                0,
                actionIconRectangle.width);

            float xCoordinate = data.barDecreaseDirection == BarDecreaseDirection.LeftToRight ? actionIconRectangle.x + actionIconRectangle.width - overlayWidth : actionIconRectangle.x;

            return new Rect(
                    xCoordinate,
                    actionIconRectangle.y,
                    overlayWidth,
                    actionIconRectangle.height);
        }
    }
}
