using NUnit.Framework;
using System;

namespace Industree.Logic.StateManager.Test
{
    public class HealthManagerTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenHealthManagerIsCreatedWithNegativeInitialHitPointsThenArgumentExceptionIsThrown()
        {
            new HealthManager(-1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenHealthManagerIsInstantiatedWithZeroHitPointsThenArgumentExceptionIsThrown()
        {
            new HealthManager(0);
        }

        [Test]
        [TestCase(1, 1, Result=0)]
        [TestCase(0, -1, ExpectedException=typeof(ArgumentException))]
        [TestCase(1, 2, Result=0)]
        [TestCase(10, 3, Result=7)]
        public int TakeDamageHitPointsTest(int initialHitPoints, int damagePoints)
        {
            HealthManager healthManager = new HealthManager(initialHitPoints);

            healthManager.TakeDamage(damagePoints);

            return healthManager.HitPoints;
        }

        [Test]
        public void WhenHitPointsOfHealthManagerAreGreaterThanZeroThenIsDeadIsFalse()
        {
            HealthManager healthManager = new HealthManager(1);

            Assert.That(!healthManager.IsDead);
        }

        [Test]
        public void GivenHealthManagerHasMoreThanZeroHitPointsWhenHitPointsOfHealthManagerReachZeroThenIsDeadIsTrue()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.TakeDamage(1);

            Assert.That(healthManager.IsDead);
        }

        [Test]
        public void WhenDieIsCalledThenHitPointsOfHealthManagerAreZero()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Die();

            Assert.AreEqual(0, healthManager.HitPoints);
        }

        [Test]
        public void WhenDieIsCalledThenIsDeadIsTrue()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Die();

            Assert.That(healthManager.IsDead);
        }

        [Test]
        public void WhenDieIsCalledThenDeathEventIsThrown()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Death += Assert.Pass;

            healthManager.Die();

            Assert.Fail();
        }

        [Test]
        public void WhenHealthManagerTakesMoreDamageThanHeHasHitPointsThenDeathEventIsThrown()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Death += Assert.Pass;

            healthManager.TakeDamage(2);

            Assert.Fail();
        }

        [Test]
        public void WhenHitPointsOfHealthManagerReachZeroThenDeathEventIsThrown()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Death += Assert.Pass;

            healthManager.TakeDamage(1);

            Assert.Fail();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenHitPointsOfHealthManagerReachZeroAndDieIsCalledAgainThenInvalidOperationExceptionIsThrown()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.TakeDamage(1);

            healthManager.Die();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenDieIsCalledTwiceThenInvalidOperationExceptionIsThrown()
        {
            HealthManager healthManager = new HealthManager(1);

            healthManager.Die();
            healthManager.Die();
        }
    }
}
