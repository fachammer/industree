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
        public int cost;
        public float cooldown;
        public int index;
        public IActionViewData actionViewData;
        public IActionCooldownViewData actionCooldownViewData;
        public ISelectedActionViewData selectedActionViewData;
        public IActionDeniedViewData actionDeniedViewData;

        private Timer cooldownTimer;

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
        public ITexture Icon { get { return actionViewData.Icon; } }
        public ITexture CooldownOverlayIcon { get { return actionCooldownViewData.IconOverlay; } }
        public ITexture DeniedOverlayIcon { get { return actionDeniedViewData.IconOverlay; } }
        public ITexture SelectedOverlayIcon { get { return selectedActionViewData.IconOverlay; } }
        public float DeniedOverlayIconTime { get { return actionDeniedViewData.OverlayTime; } }

        private void StartCooldown()
        {
            cooldownTimer = Timer.Start(cooldown, StopCooldownTimer);
        }

        private void StopCooldownTimer(ITimer timer)
        {
            timer.Stop();
            cooldownTimer = null;
        }

        public virtual void Invoke(IPlayer player, float actionDirection)
        {
            StartCooldown();
            PerformInvoke(player, actionDirection);
        }

        protected abstract void PerformInvoke(IPlayer player, float actionDirection);

        public virtual bool IsInvokable(IPlayer player, float actionDirection) { return true; }


        
    }
}
