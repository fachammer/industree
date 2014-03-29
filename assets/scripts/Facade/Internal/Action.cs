using Industree.Data.View;
using Industree.Facade;
using Industree.Graphics;
using Industree.Time;
using Industree.Time.Internal;
using Industree.View;
using System;
using UnityEngine;

namespace Industree.Facade.Internal
{
    internal abstract class Action : MonoBehaviour, IAction
    {
        public int cost = 0;
        public float cooldown = 0;
        public int index = 0;
        public ActionIconViewData actionViewData = null;
        public ActionCooldownViewData actionCooldownViewData = null;
        public ActionDeniedViewData actionDeniedViewData = null;

        private ITimer cooldownTimer;
        private ActionView actionView;

        public event Action<IPlayer, IAction, float> Failure = (player, action, direction) => { };

        public int Cost { get { return cost; } }
        public float Cooldown { get { return cooldown; } }
        public int Index { get { return index; } }
        public bool IsCoolingDown { get { return cooldownTimer != null; } }

        public float RemainingCooldown
        {
            get
            {
                if (cooldownTimer != null)
                    return cooldown - cooldownTimer.TimeSinceLastTick;
                return 0;
            }
        }

        public Rect IconBounds { get { return actionViewData.IconBounds; } }
        public Rect CostBounds { get { return actionViewData.CostBounds; } }
        public Texture Icon { get { return actionViewData.Icon; } }
        public Texture CooldownOverlayIcon { get { return actionCooldownViewData.IconOverlay; } }
        public BarDecreaseDirection CooldownDecreaseDirection { get { return actionCooldownViewData.CooldownOverlayDecreaseDirection; } }
        public Texture DeniedOverlayIcon { get { return actionDeniedViewData.IconOverlay; } }
        public float DeniedOverlayIconTime { get { return actionDeniedViewData.OverlayTime; } }

        private void StartCooldown()
        {
            cooldownTimer = Timing.GetTimerFactory().GetTimer(cooldown);
            cooldownTimer.Tick += StopCooldownTimer;
        }

        private void StopCooldownTimer(ITimer timer)
        {
            timer.Stop();
            cooldownTimer = null;
        }

        public void Invoke(IPlayer player, float actionDirection)
        {
            StartCooldown();
            PerformInvoke(player, actionDirection);
        }

        public void Fail(IPlayer player, float actionDirection)
        {
            Failure(player, this, actionDirection);
        }

        protected abstract void PerformInvoke(IPlayer player, float actionDirection);

        public virtual bool IsInvokable(IPlayer player, float actionDirection) { return true; }

        private void Awake()
        {
            actionView = new ActionView(this, GuiRendererFactory.GetResolutionIndependentRenderer(), actionViewData.ViewSkin);
        }

        private void OnGUI()
        {
            actionView.Draw();
        }
    }
}
