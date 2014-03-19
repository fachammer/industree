using Industree.Facade;
using Industree.Model;
using UnityEngine;

public class ActionEntity : MonoBehaviour {

	private IPlayer player;
	private float actionDirection;

    public IPlayer Player
    { 
		get { return player; }
		set { player = value; } 
	}

	public float ActionDirection { 
		get { return actionDirection; }
		set { actionDirection = value; } 
	}
}