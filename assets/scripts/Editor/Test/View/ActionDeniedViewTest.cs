using NUnit.Framework;
using NSubstitute;
using Industree.Time;
using Industree.Facade;
using Industree.Graphics;
using System;
using UnityEngine;

namespace Industree.View.Test
{
    public class ActionDeniedViewTest
    {
        [Test]
        public void WhenActionFailsAndDrawIsCalledThenGuiRecievesCallToDrawTexture()
        {
            IAction action = Substitute.For<IAction>();
            action.IconBounds.Returns(new Rect());
            action.DeniedOverlayIcon.Returns(Substitute.For<Texture>());
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionDeniedView actionDeniedView = new ActionDeniedView(action, Substitute.For<ITimerFactory>(), gui, Substitute.For<IViewSkin>());

            action.Failure += Raise.Event<Action<IPlayer, IAction, float>>(Substitute.For<IPlayer>(), action, 1f);

            actionDeniedView.Draw();

            gui.Received().DrawTexture(action.DeniedOverlayIcon, action.IconBounds);
        }

        [Test]
        public void WhenActionDoesNotFailAndDrawIsCalledThenGuiDoesNotRecieveCallToDrawTexture()
        {
            IAction action = Substitute.For<IAction>();
            action.IconBounds.Returns(new Rect());
            action.DeniedOverlayIcon.Returns(Substitute.For<Texture>());
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionDeniedView actionDeniedView = new ActionDeniedView(action, Substitute.For<ITimerFactory>(), gui, Substitute.For<IViewSkin>());

            actionDeniedView.Draw();

            gui.DidNotReceiveWithAnyArgs().DrawTexture(action.DeniedOverlayIcon, action.IconBounds);
        }

        [Test]
        public void WhenActionFailsAndTimerTickedAndDrawIsCalledThenGuiDoesNotRecieveCallToDrawTexture()
        {
            IAction action = Substitute.For<IAction>();
            ITimerFactory timerFactory = Substitute.For<ITimerFactory>();
            ITimer fakeTimer = Substitute.For<ITimer>();
            timerFactory.GetTimer(action.DeniedOverlayIconTime).ReturnsForAnyArgs(fakeTimer);
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionDeniedView actionDeniedView = new ActionDeniedView(action, timerFactory, gui, Substitute.For<IViewSkin>());

            action.Failure += Raise.Event<Action<IPlayer, IAction, float>>(Substitute.For<IPlayer>(), action, 1f);
            fakeTimer.Tick += Raise.Event<Action<ITimer>>(fakeTimer);
            actionDeniedView.Draw();

            gui.DidNotReceiveWithAnyArgs().DrawTexture(null, new Rect());
        }
    }
}
