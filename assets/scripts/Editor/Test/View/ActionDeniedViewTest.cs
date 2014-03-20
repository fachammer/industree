using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;
using System;
using UnityEngine;
using Industree.Logic.Test;
using Industree.Time;

namespace Industree.View.Test
{
    public class ActionDeniedViewTest
    {
        [Test]
        public void GivenActionDeneidViewIsInstantiatedWhenActionWasDeniedAndDrawIsCalledThenDrawTextureOnGuiRendererIsCalledOnceAfterOneTick()
        {
            IPlayer player = Substitute.For<IPlayer>();
            IAction failedAction = Substitute.For<IAction>();
            ITexture fakeTexture = Substitute.For<ITexture>();
            failedAction.DeniedOverlayIcon.Returns(fakeTexture);
            failedAction.IconBounds.Returns(new Rect(0, 0, 1, 1));
            failedAction.DeniedOverlayIconTime.Returns(1);

            IClock clock = Substitute.For<IClock>();
            TestTimer fakeTimer = new TestTimer();

            clock.WhenForAnyArgs(c => c.CallbackOnce(0, null))
                .Do(callInfo => {
                    fakeTimer.Tick += (t) => callInfo.Arg<Action>()();
                });

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            ActionDeniedView actionDeniedView = new ActionDeniedView(player, clock, gui);

            player.ActionFailure += Raise.Event<Action<IPlayer, IAction, float>>(player, failedAction, 1f);


            actionDeniedView.Draw();
            fakeTimer.SimulateOneTick();
            actionDeniedView.Draw();


            gui.Received(1).DrawTexture(fakeTexture, new Rect(0, 0, 1, 1));
        }
    }
}
