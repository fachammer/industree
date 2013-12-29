using System;
using UnityEngine;

public class Action: MonoBehaviour
{
    public int cost;
	public float cooldownTime;
	public Texture2D icon;

	private int index;

	public int Index {
		get { return index; }
		set { index = value; }
	}

    public virtual void Perform(Player player, float actionDirection){}

	public virtual bool IsPerformable(Player player, float actionDirection){
		return true;
	}
}
