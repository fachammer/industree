using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Interactive:MonoBehaviour
{
    public Texture2D icon;
    public int cost = 10;
	public float cooldownTime = 0f;

    public virtual bool performAction(Player player, float castDirection){
		return true;
	}
}

