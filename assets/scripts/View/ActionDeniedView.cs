using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets.scripts.Rendering;
using assets.scripts.Controller;

namespace assets.scripts.View
{
    [RequireComponent(typeof(ActionView))]
    public class ActionDeniedView : View<ActionDeniedViewData>
    {
        private ActionInvoker actionInvoker;
        private Timer actionDeniedOverlayTimer;
        private ActionView actionView;
        private Action action;

        private void Awake()
        {
            actionInvoker = transform.parent.GetComponent<ActionInvoker>();
            actionInvoker.ActionFailure += OnPlayerActionFailure;
            actionView = GetComponent<ActionView>();
            action = GetComponent<Action>();
        }

        private void OnPlayerActionFailure(Player player, Action action, float actionDirection)
        {
            if (this.action == action)
            {
                HandleActionFailure();
            }
        }

        private void HandleActionFailure()
        {
            if (actionDeniedOverlayTimer != null)
            {
                actionDeniedOverlayTimer.Stop();
            }

            actionDeniedOverlayTimer = Timer.Start(data.overlayTime,
                (timer) => {
                    timer.Stop();
                    actionDeniedOverlayTimer = null;
                });
        }

        protected override void Draw()
        {
            if (actionDeniedOverlayTimer != null)
            {
                ResolutionIndependentRenderer.DrawTexture(actionView.data.bounds, data.deniedOverlay);
            }
        }
    }
}