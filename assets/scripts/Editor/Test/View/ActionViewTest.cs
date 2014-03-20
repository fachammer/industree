using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using UnityEngine;
using Industree.Graphics;
using Industree.Data.View;

namespace Industree.View.Test
{
    public class ActionViewTest
    {
        [Test]
        public void GivenActionViewIsInstantiatedWhenDrawIsCalledThenGuiRendererRecievesCallToDrawTexture()
        {
            IActionViewData actionViewData = Substitute.For<IActionViewData>();
            Rect actionIconBounds = new Rect(1, 1, 1, 1);
            actionViewData.IconBounds.Returns(actionIconBounds);
            ITexture fakeTexture = Substitute.For<ITexture>();
            actionViewData.Icon.Returns(fakeTexture);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionView actionView = new ActionView(Substitute.For<IAction>(), actionViewData, gui);

            actionView.Draw();

            gui.Received().DrawTexture(fakeTexture, actionIconBounds);
        }

        [Test]
        public void GivenActionViewIsInstantiatedWhenDrawIsCalledThenGuiRendererRecievesCallToDrawText()
        {
            IAction action = Substitute.For<IAction>();
            action.Cost.Returns(1);

            IActionViewData actionViewData = Substitute.For<IActionViewData>();
            Rect actionCostBounds = new Rect(1, 1, 1, 1);
            actionViewData.CostBounds.Returns(actionCostBounds);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionView actionView = new ActionView(action, actionViewData, gui);

            actionView.Draw();

            gui.Received().DrawText(1.ToString(), actionCostBounds);
        }
    }
}
