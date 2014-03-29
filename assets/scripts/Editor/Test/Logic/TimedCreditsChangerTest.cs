using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Time;
using System;

namespace Industree.Logic.Test
{
    public class TimedCreditsChangerTest
    {
        [Test]
        public void WhenTimedCreditsIncreaserIsCreatedThenCreditsOfPlayerIncreaseWithEachTick()
        {
            IPlayer player = Substitute.For<IPlayer>();
            ITimerFactory timerFactory = Substitute.For<ITimerFactory>();
            ITimer fakeTimer = Substitute.For<ITimer>();
            timerFactory.GetTimer(1f).Returns(fakeTimer);
            ValuePerInterval<int> creditsPerInterval = new ValuePerInterval<int>(1, 1);
            TimedCreditsChanger creditsIncreaser = new TimedCreditsChanger(player, creditsPerInterval, timerFactory);

            fakeTimer.Tick += Raise.Event<Action<ITimer>>(fakeTimer);

            player.Received().IncreaseCredits(1);
        }

        [Test]
        public void WhenTimedCreditsIncreaserIsCreatedWithNegativeValuePerIntervalThenCreditsOfPlayerDecreaseWithEachTick()
        {
            IPlayer player = Substitute.For<IPlayer>();
            ITimerFactory timerFactory = Substitute.For<ITimerFactory>();
            ITimer fakeTimer = Substitute.For<ITimer>();
            timerFactory.GetTimer(1f).Returns(fakeTimer);
            ValuePerInterval<int> creditsPerInterval = new ValuePerInterval<int>(-1, 1);
            TimedCreditsChanger creditsIncreaser = new TimedCreditsChanger(player, creditsPerInterval, timerFactory);

            fakeTimer.Tick += Raise.Event<Action<ITimer>>(fakeTimer);

            player.Received().DecreaseCredits(1);
        }

        [Test]
        public void WhenTimedCreditsIncreaserIsCreatedWithZeroValuePerIntervalThenPlayerDoesNotRecieveCallToIncreaseOrDecreaseCredits()
        {
            IPlayer player = Substitute.For<IPlayer>();
            ITimerFactory timerFactory = Substitute.For<ITimerFactory>();
            ITimer fakeTimer = Substitute.For<ITimer>();
            timerFactory.GetTimer(1f).Returns(fakeTimer);
            ValuePerInterval<int> creditsPerInterval = new ValuePerInterval<int>(0, 1);
            TimedCreditsChanger creditsIncreaser = new TimedCreditsChanger(player, creditsPerInterval, timerFactory);

            fakeTimer.Tick += Raise.Event<Action<ITimer>>(fakeTimer);

            player.DidNotReceiveWithAnyArgs().IncreaseCredits(0);
            player.DidNotReceiveWithAnyArgs().DecreaseCredits(0);
        }
    }
}
