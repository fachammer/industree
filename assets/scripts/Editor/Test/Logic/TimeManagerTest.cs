using NUnit.Framework;
using NSubstitute;
using Industree.Time;
using Industree.Facade;
using System;

namespace Industree.Logic.Test
{
    public class TimeManagerTest
    {
        [Test]
        public void GivenTimeManagerIsInstantiatedWhenGamePauseEventIsThrownThenTimerIsPaused()
        {
            IGame game = Substitute.For<IGame>();
            ITimer timer = Substitute.For<ITimer>();
            new TimeManager(timer, game);

            game.GamePause += Raise.Event<Action>();

            timer.Received().Pause();
        }

        [Test]
        public void GivenTimeManagerIsInstantiatedWhenGameResumeEventIsThrownThenTimerIsResumed()
        {
            IGame game = Substitute.For<IGame>();
            ITimer timer = Substitute.For<ITimer>();
            new TimeManager(timer, game);

            game.GameResume += Raise.Event<Action>();

            timer.Received().Resume();
        }

        [Test]
        public void GivenTimerManagerIsInstantiatedWhenGameEndEventIsThrownThenTimerIsPaused()
        {
            IGame game = Substitute.For<IGame>();
            ITimer timer = Substitute.For<ITimer>();
            new TimeManager(timer, game);

            game.GameEnd += Raise.Event<Action>();

            timer.Received().Pause();
        }
    }
}
