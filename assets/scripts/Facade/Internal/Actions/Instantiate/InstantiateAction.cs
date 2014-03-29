using UnityEngine;
using Industree.Facade;
using Industree.Facade.Internal;

namespace Industree.Model.Actions
{
    internal class InstantiateAction : Action
    {
        public GameObject actionGameEntity = null;

        protected override void PerformInvoke(IPlayer player, float actionDirection)
        {
            InstantiateActionEntity(player, actionDirection);
        }

        protected GameObject InstantiateActionEntity(IPlayer player, float actionDirection)
        {
            ActionEntity actionEntity = ((GameObject)Instantiate(actionGameEntity)).GetComponent<ActionEntity>();
            actionEntity.Player = player;
            actionEntity.ActionDirection = actionDirection;
            return actionEntity.gameObject;
        }
    }
}
