using UnityEngine;
using System.Collections;

public abstract class InstantiateOnPositionAction : InstantiateAction {

	protected override void PerformInvoke(Player player, float actionDirection){
		GameObject actionEntity = base.InstantiateActionEntity(player, actionDirection);
		Vector3 position = GetInitialActionEntityPosition(player, actionDirection);
		actionEntity.transform.position = position;
	}

	protected abstract Vector3 GetInitialActionEntityPosition(Player player, float actionDirection);
}
