using UnityEngine;
using Industree.Facade;
using Industree.Facade.Internal;

namespace Industree.Model.Actions
{
    internal class TreeAction : Action
    {

        protected override void PerformInvoke(IPlayer player, float actionDirection)
        {
            TreePlacer treePlacer = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<TreePlacer>();
            TreeComponent treeComponent = treePlacer.PlaceTree(player);
            treeComponent.player = player;
        }

        public override bool IsInvokable(IPlayer player, float actionDirection)
        {
            TreePlacer treePlacer = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<TreePlacer>();
            return treePlacer.CanPlaceTree(player);
        }
    }
}