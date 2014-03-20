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
        public void GivenActionCooldownViewIsInstantiatedWhenDrawIsCalledThenDrawOnGuiRendererIsCalled(float actionCooldown, float remainingCooldown, float actionBoundsWidth, float expectedCooldownOverlayBoundsWidth){
            IAction action = Substitute.For<IAction>();
            action.IsCoolingDown.Returns(true);
            action.Cooldown.Returns(actionCooldown);
            action.RemainingCooldown.Returns(remainingCooldown);

            IActionViewData actionViewData = Substitute.For<IActionViewData>();
            actionViewData.IconBounds.Returns(new Rect(0, 0, actionBoundsWidth, 1));

            IActionCooldownViewData actionCooldownViewData = Substitute.For<IActionCooldownViewData>();
            ITexture fakeTexture = Substitute.For<ITexture>();
            actionCooldownViewData.IconOverlay.Returns(fakeTexture);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionCooldownView actionCooldownView = new ActionCooldownView(action, actionViewData, actionCooldownViewData, gui);

            actionCooldownView.Draw();

            gui.Received().DrawTexture(fakeTexture, new Rect(0, 0, expectedCooldownOverlayBoundsWidth, 1));
        }

        [Test]
        public void GivenActionIsNotCoolingDownWhenDrawIsCalledThenDrawOnGuiRendererIsNotCalled()
        {
            IAction action = Substitute.For<IAction>();
            action.IsCoolingDown.Returns(false);
            action.Cooldown.Returns(1);
            action.RemainingCooldown.Returns(0);

            IActionViewData actionViewData = Substitute.For<IActionViewData>();
            actionViewData.IconBounds.Returns(new Rect());

            IActionCooldownViewData actionCooldownViewData = Substitute.For<IActionCooldownViewData>();
            ITexture fakeTexture = Substitute.For<ITexture>();
            actionCooldownViewData.IconOverlay.Returns(fakeTexture);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionCooldownView actionCooldownView = new ActionCooldownView(action, actionViewData, actionCooldownViewData, gui);

            actionCooldownView.Draw();

            gui.DidNotReceiveWithAnyArgs().DrawTexture(null, new Rect());
        }
    }
}
