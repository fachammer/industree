using UnityEngine;

public class ActionEntity : MonoBehaviour {

	private Player player;
	private float actionDirection;

	public Player Player { 
		get { return player; }
		set { player = value; } 
	}

	public float ActionDirection { 
		get { return actionDirection; }
		set { actionDirection = value; } 
	}
}