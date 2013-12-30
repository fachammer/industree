using System;
using UnityEngine;

public class Action :MonoBehaviour {

	public int cost;
	public float cooldown;
	public Texture2D icon;

	private int index;

	public int Index {
		get { return index; }
		set { index = value; }
	}

    public virtual void Invoke(Player player, float actionDirection) {}
	public virtual bool IsInvokable(Player player, float actionDirection) { return true; }
}
