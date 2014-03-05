using System;
using UnityEngine;

public class Action : MonoBehaviour
{
    public int cost;
    public float cooldown;
    public Texture2D icon;

    public int index;
    private Timer cooldownTimer;

    public bool IsCoolingDown { get { return cooldownTimer != null; } }

    private void StartCooldown()
    {
        cooldownTimer = Timer.AddTimerToGameObject(gameObject, cooldown, StopCooldownTimer);
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

    protected virtual void PerformInvoke(Player player, float actionDirection){ }

    public virtual bool IsInvokable(Player player, float actionDirection) { return true; }
}
