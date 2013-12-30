using UnityEngine;
using System.Collections;

public class TreeAction : Action {

	public override void Invoke(Player player, float actionDirection){
		Planet planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		planet.placeTree(player);
	}

	public override bool IsInvokable(Player player, float actionDirection){
		Planet planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		return planet.CanPlaceTree(player);
	}
}
