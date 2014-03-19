using System;
using Industree.Facade;
using NSubstitute;
using NUnit.Framework;

namespace Industree.Logic.Test
{
    public class CreditsManagerTest
    {
        [Test]
        [TestCase(0, 0, Result=0)]
        [TestCase(0, 1, Result=1)]
        [TestCase(0, 2, Result=2)]
        [TestCase(1, 1, Result=2)]
        [TestCase(1, 2, Result=3)]
        [TestCase(0, -1, ExpectedException=typeof(ArgumentException))]
        public int IncreaseCreditsByAmountTest(int initialCreditsAmount, int increaseAmount)
        {
            CreditsManager creditsManager = new CreditsManager(initialCreditsAmount);

            creditsManager.IncreaseCreditsByAmount(increaseAmount);

            return creditsManager.Credits;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreditsManagerIsCreatedWithNegativeAmountOfCreditsThenArgumentExceptionIsThrown()
        {
            new CreditsManager(-1);
        }

        [Test]
        [TestCase(0, 0, Result=0)]
        [TestCase(1, 1, Result=0)]
        [TestCase(1, 2, ExpectedException=typeof(ArgumentException))]
        [TestCase(0, -1, ExpectedException=typeof(ArgumentException))]
        public int DecreaseCreditsByAmountTest(int initialCreditsAmount, int decreaseAmount)
        {
            CreditsManager creditsManager = new CreditsManager(initialCreditsAmount);

            creditsManager.DecreaseCreditsByAmount(decreaseAmount);

            return creditsManager.Credits;
        }

        [Test]
        public void WhenIncreaseCreditsByAmountIsCalledThenCreditsChangeEventIsThrownWithCorrectOldCredits()
        {
            CreditsManager creditsManager = new CreditsManager(0);
            creditsManager.CreditsChange += (oldCredits, newCredits) => {
                Assert.AreEqual(0, oldCredits);
                Assert.Pass();
            };

            creditsManager.IncreaseCreditsByAmount(1);

            Assert.Fail();
        }

        [Test]
        public void WhenIncreaseCreditsByAmountIsCalledThenCreditsChangeEventIsThrownWithCorrectNewCredits()
        {
            CreditsManager creditsManager = new CreditsManager(0);
            creditsManager.CreditsChange += (oldCredits, newCredits) => {
                Assert.AreEqual(1, newCredits);
                Assert.Pass();
            };

            creditsManager.IncreaseCreditsByAmount(1);

            Assert.Fail();
        }

        [Test]
        public void WhenDecreaseCreditsByAmountIsCalledThenCreditsChangeEventIsThrownWithCorrectOldCredits()
        {
            CreditsManager creditsManager = new CreditsManager(1);
            creditsManager.CreditsChange += (oldCredits, newCredits) => {
                Assert.AreEqual(1, oldCredits);
                Assert.Pass();
            };

            creditsManager.DecreaseCreditsByAmount(1);

            Assert.Fail();
        }
    }
}
