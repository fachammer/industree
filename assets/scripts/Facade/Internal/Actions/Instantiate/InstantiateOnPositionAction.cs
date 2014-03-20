using UnityEngine;
using System.Collections;
using Industree.Facade;

namespace Industree.Model.Actions {
    internal abstract class InstantiateOnPositionAction : InstantiateAction
    {

        protected override void PerformInvoke(IPlayer player, float actionDirection)
        {
            GameObject actionEntity = base.InstantiateActionEntity(player, actionDirection);
            Vector3 position = GetInitialActionEntityPosition(player, actionDirection);
            actionEntity.transform.position = position;
        }

        protected abstract Vector3 GetInitialActionEntityPosition(IPlayer player, float actionDirection);
    }
}