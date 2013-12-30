using UnityEngine;
using System.Collections;

public class InstantiateAction : Action {

	public GameObject actionGameEntity;

	public override void Invoke(Player player, float actionDirection){
		InstantiateActionEntity(player, actionDirection);
	}

	protected GameObject InstantiateActionEntity(Player player, float actionDirection){
		ActionEntity actionEntity = ((GameObject) Instantiate(actionGameEntity)).GetComponent<ActionEntity>();
		actionEntity.Player = player;
		actionEntity.ActionDirection = actionDirection;
		return actionEntity.gameObject;
	}
}
