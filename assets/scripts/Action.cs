using System;
using UnityEngine;


public class Action: MonoBehaviour
{
    public int cost;
	public float cooldownTime;
	public Texture2D icon;

	private Timer cooldownTimer;

    public virtual bool performAction(Player player, float actionDirection){
		return true;
	}

	public bool Act(Player player, float actionDirection){
		if(cooldownTimer != null){
			return false;
		}

		cooldownTimer = Timer.Instantiate(cooldownTime, OnCooldownTimerTick);

		return performAction(player, actionDirection);
	}

	private void OnCooldownTimerTick(Timer timer){
		timer.Stop();
		cooldownTimer = null;
	}
}

