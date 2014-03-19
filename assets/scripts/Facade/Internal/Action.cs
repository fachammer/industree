using Industree.Facade;
using Industree.View;
using System;
using UnityEngine;

namespace Industree.Facade.Internal
{
    public abstract class Action : MonoBehaviour, IAction
    {
        public int cost;
        public float cooldown;
        public int index;

        private Timer cooldownTimer;

        public int Cost { get { return cost; } }
        public float Cooldown { get { return cooldown; } }

        public int Index { get { return index; } }
        public bool IsCoolingDown { get { return cooldownTimer != null; } }

        public Rect IconBounds { get { return actionView.data.bounds; } }

        private ActionView actionView;

        private void Awake()
        {
            actionView = GetComponent<ActionView>();
        }

        private void StartCooldown()
        {
            cooldownTimer = Timer.Start(cooldown, StopCooldownTimer);
        }

        private void StopCooldownTimer(Timer timer)
        {
            timer.Stop();
            cooldownTimer = null;
        }

        public virtual void Invoke(IPlayer player, float actionDirection)
        {
            StartCooldown();
            PerformInvoke(player, actionDirection);
        }

        public float GetRemainingCooldown()
        {
            if (cooldownTimer != null)
            {
                return cooldown - cooldownTimer.TimeSinceLastTick;
            }
            return 0;
        }

        protected abstract void PerformInvoke(IPlayer player, float actionDirection);

        public virtual bool IsInvokable(IPlayer player, float actionDirection) { return true; }
    }
}
