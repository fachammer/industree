using System;
using UnityEngine;


public class Interactive:MonoBehaviour
{
    public int cost;
	public float cooldownTime;
	public Texture2D icon;

    public virtual bool performAction(Player player, float castDirection){
		return true;
	}
}

