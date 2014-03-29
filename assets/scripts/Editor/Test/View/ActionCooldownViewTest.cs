using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View.Test
{
    public class ActionCooldownViewTest
    {
        [Test]
        [TestCase(1, 1, 10, 10)]
        [TestCase(1, 0.5f, 10, 5)]
        public void GivenActionCooldownViewIsInstantiatedWhenDrawIsCalledThenDrawTextureOnGuiRendererIsCalled(float actionCooldown, float remainingCooldown, float actionBoundsWidth, float expectedCooldownOverlayBoundsWidth){
            IAction action = Substitute.For<IAction>();
            action.IsCoolingDown.Returns(true);
            action.Cooldown.Returns(actionCooldown);
            action.RemainingCooldown.Returns(remainingCooldown);
            action.CooldownDecreaseDirection.Returns(BarDecreaseDirection.RightToLeft);
            action.IconBounds.Returns(new Rect(0, 0, actionBoundsWidth, 1));
            action.CooldownOverlayIcon.Returns(Substitute.For<Texture>());

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionCooldownView actionCooldownView = new ActionCooldownView(action, gui, Substitute.For<IViewSkin>());

            actionCooldownView.Draw();

            gui.Received().DrawTexture(action.CooldownOverlayIcon, new Rect(0, 0, expectedCooldownOverlayBoundsWidth, 1));
        }

        [Test]
        [TestCase(1, 1, 0, BarDecreaseDirection.LeftToRight, 0, 1)]
        [TestCase(1, 0.5f, 0, BarDecreaseDirection.LeftToRight, 0.5f, 0.5f)]
        [TestCase(1, 1, 0, BarDecreaseDirection.RightToLeft, 0, 1)]
        [TestCase(1, 0.5f, 0, BarDecreaseDirection.RightToLeft, 0, 0.5f)]
        public void GivenCooldownDecreaseDirectionWhenDrawIsCalledThenDrawOnGuiRendererIsCalled(float actionCooldown, float remainingCooldown, float actionBoundsX, BarDecreaseDirection cooldownDecreaseDirection, float expectedCooldownOverlayBoundsX, float expectedCooldownOverlayBoundsWidth)
        {
            IAction action = Substitute.For<IAction>();
            action.IsCoolingDown.Returns(true);
            action.Cooldown.Returns(actionCooldown);
            action.RemainingCooldown.Returns(remainingCooldown);
            action.IconBounds.Returns(new Rect(actionBoundsX, 0, 1, 1));
            action.CooldownOverlayIcon.Returns(Substitute.For<Texture>());
            action.CooldownDecreaseDirection.Returns(cooldownDecreaseDirection);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionCooldownView actionCooldownView = new ActionCooldownView(action, gui, Substitute.For<IViewSkin>());

            actionCooldownView.Draw();

            gui.Received().DrawTexture(action.CooldownOverlayIcon, new Rect(expectedCooldownOverlayBoundsX, 0, expectedCooldownOverlayBoundsWidth, 1));
        }

        [Test]
        public void GivenActionIsNotCoolingDownWhenDrawIsCalledThenDrawOnGuiRendererIsNotCalled()
        {
            IAction action = Substitute.For<IAction>();
            action.IsCoolingDown.Returns(false);
            action.Cooldown.Returns(1);
            action.RemainingCooldown.Returns(0);
            action.IconBounds.Returns(new Rect());
            action.CooldownOverlayIcon.Returns(Substitute.For<Texture>());

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionCooldownView actionCooldownView = new ActionCooldownView(action, gui, Substitute.For<IViewSkin>());

            actionCooldownView.Draw();

            gui.DidNotReceiveWithAnyArgs().DrawTexture(null, new Rect());
        }
    }
}
