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
            IAction action = Substitute.For<IAction>();
            action.IconBounds.Returns(new Rect(1, 1, 1, 1));
            action.Icon.Returns(Substitute.For<Texture>());
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionIconView actionView = new ActionIconView(action, gui, Substitute.For<IViewSkin>());

            actionView.Draw();

            gui.Received().DrawTexture(action.Icon, action.IconBounds);
        }

        [Test]
        public void GivenActionViewIsInstantiatedWhenDrawIsCalledThenGuiRendererRecievesCallToDrawText()
        {
            IAction action = Substitute.For<IAction>();
            action.Cost.Returns(1);
            action.CostBounds.Returns(new Rect(1, 1, 1, 1));

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            IViewSkin skin = Substitute.For<IViewSkin>();
            ActionIconView actionView = new ActionIconView(action, gui, Substitute.For<IViewSkin>());

            actionView.Draw();

            gui.Received().DrawText(1.ToString(), action.CostBounds, skin.Label);
        }
    }
}
