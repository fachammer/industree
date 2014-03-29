using NUnit.Framework;
using NSubstitute;
using System;

namespace Industree.Logic.StateManager.Test
{
    public class PollutionManagerTest
    {
        [Test]
        public void WhenPollutionManagerIsInstantiatedWithMaximumPollutionThenMaximumPollutionIsGivenPollution()
        {
            PollutionManager pollutionManager = new PollutionManager(1);

            Assert.AreEqual(1, pollutionManager.MaximumPollution);
        }

        [Test]
        public void WhenPollutionManagerIsInstantiatedWithMaximumPollutionThenCurrentPollutionIsZero()
        {
            PollutionManager pollutionManager = new PollutionManager(1);

            Assert.AreEqual(0, pollutionManager.CurrentPollution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPollutionManagerIsInstantiatedWithZeroMaximumPollutionThenArgumentExceptionIsThrown()
        {
            new PollutionManager(0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPollutionManagerIsInstantiatedWithnegativeMaximumPollutionThenArgumentExceptionIsThrown()
        {
            new PollutionManager(-1);
        }

        [Test]
        public void WhenPollutionManagerIsInstantiatedWithMaximumPollutionAndCurrentPollutionThenMaximumPollutionAndCurrentPollutionAreGivenValues()
        {
            PollutionManager pollutionManager = new PollutionManager(1, 1);

            Assert.AreEqual(1, pollutionManager.MaximumPollution);
            Assert.AreEqual(1, pollutionManager.CurrentPollution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPollutionManagerIsInstantiatedWithNegativeCurrentPollutionThenArgumentExceptionIsThrown()
        {
            new PollutionManager(1, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPollutionManagerIsInstantiatedWithCurrentPollutionGreaterThanMaximumPollutionThenArgumentExceptionIsThrown()
        {
            new PollutionManager(1, 2);
        }

        [Test]
        public void WhenIncreasePollutionIsCalledThenCurrentPollutionIsIncreasedByGivenValue()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 0);

            pollutionManager.IncreasePollutionByAmount(1);

            Assert.AreEqual(1, pollutionManager.CurrentPollution);
        }

        [Test]
        public void WhenIncreasePollutionIsCalledAndWouldRaiseAboveMaximumPollutionThenCurrentPollutionEqualsMaximumPollution()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 1);

            pollutionManager.IncreasePollutionByAmount(2);

            Assert.AreEqual(pollutionManager.MaximumPollution, pollutionManager.CurrentPollution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenIncreasePollutionIsCalledWithNegativePollutionAmountThenArgumentExceptionIsThrown()
        {
            new PollutionManager(1, 0).IncreasePollutionByAmount(-1);
        }

        [Test]
        public void WhenDecreasePollutionIsCalledThenCurrentPollutionIsDecreasedByGivenValue()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 2);

            pollutionManager.DecreasePollutionByAmount(1);

            Assert.AreEqual(1, pollutionManager.CurrentPollution);
        }

        [Test]
        public void WhenDecreasePollutionIsCalledAndWouldFallBelowZeroThenCurrentPollutionIsZero()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 1);

            pollutionManager.DecreasePollutionByAmount(2);

            Assert.AreEqual(0, pollutionManager.CurrentPollution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenDecreasePollutionIsCalledWithNegativeAmoundThenArgumentExceptionIsThrown()
        {
            new PollutionManager(2, 1).DecreasePollutionByAmount(-1);
        }

        [Test]
        public void WhenIncreasePollutionIsCalledAndReachesMaximumPollutionThenMaximumPollutionReachedEventIsThrown()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 1);
            pollutionManager.MaximumPollutionReached += Assert.Pass;

            pollutionManager.IncreasePollutionByAmount(1);

            Assert.Fail();
        }

        [Test]
        public void WhenDecreasePollutionIsCalledAndCurrentPollutionReachesZeroThenZeroPollutionReachedEventIsThrown()
        {
            PollutionManager pollutionManager = new PollutionManager(2, 1);
            pollutionManager.ZeroPollutionReached += Assert.Pass;

            pollutionManager.DecreasePollutionByAmount(1);

            Assert.Fail();
        }
    }
}
