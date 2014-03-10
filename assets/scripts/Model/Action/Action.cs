using System;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public int cost;
    public float cooldown;
    public int index;

    private Timer cooldownTimer;

    public bool IsCoolingDown { get { return cooldownTimer != null; } }

    private void StartCooldown()
    {
        cooldownTimer = Timer.Start(cooldown, StopCooldownTimer);
    }

    private void StopCooldownTimer(Timer timer)
    {
        timer.Stop();
        cooldownTimer = null;
    }

    public virtual void Invoke(Player player, float actionDirection)
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

    protected abstract void PerformInvoke(Player player, float actionDirection);

    public virtual bool IsInvokable(Player player, float actionDirection) { return true; }
}
