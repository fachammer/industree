using UnityEngine;
using Industree.Rendering;
using Industree.Logic;
using Industree.Facade;
using Industree.Facade.Internal;

namespace Industree.View
{
    [RequireComponent(typeof(ActionView))]
    public class ActionDeniedView : View<ActionDeniedViewData>
    {
        private IActionInvoker actionInvoker;
        private Timer actionDeniedOverlayTimer;
        private ActionView actionView;
        private IAction action;

        private void Awake()
        {
            throw new System.NotImplementedException("change to non-MonoBehaviour");
            // actionInvoker = transform.parent.GetComponent<Player>().ActionInvoker;
            // actionInvoker.ActionFailure += OnPlayerActionFailure;
            actionView = GetComponent<ActionView>();
            action = GetComponent<Action>();
        }

        private void OnPlayerActionFailure(IPlayer player, IAction action, float actionDirection)
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