using UnityEngine;
using System.Collections;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    

    [RequireComponent(typeof(Action))]
    [RequireComponent(typeof(ActionView))]
    public class ActionCooldownView : View<ActionCooldownViewData>
    {
        private Action action;
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

        private Rect CalculateCooldownOverlayRectangle(Action action)
        {
            Rect actionIconRectangle = actionView.data.bounds;
            float overlayWidth = Mathf.Clamp(
                action.GetRemainingCooldown() * actionIconRectangle.width / action.cooldown,
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
