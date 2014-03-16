using UnityEngine;
using System.Collections;

public class TreeAction : Action {

	protected override void PerformInvoke(Player player, float actionDirection){
		TreePlacer treePlacer = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<TreePlacer>();
		TreeComponent treeComponent = treePlacer.PlaceTree(player);
		treeComponent.player = player;
	}

	public override bool IsInvokable(Player player, float actionDirection){
		TreePlacer treePlacer = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<TreePlacer>();
		return treePlacer.CanPlaceTree(player);
	}
}
