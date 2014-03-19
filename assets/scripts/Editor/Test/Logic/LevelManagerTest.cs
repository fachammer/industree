using NUnit.Framework;
using Industree.Logic;
using System;

namespace Industree.Logic.Test
{
    public class LevelManagerTest
    {
        [Test]
        [TestCase(-1, ExpectedException=typeof(ArgumentException))]
        [TestCase(0, Result=1)]
        [TestCase(1, Result=2)]
        public int RaiseLevelTest(int initialLevel)
        {
            LevelManager levelManager = new LevelManager(initialLevel);

            levelManager.RaiseLevel();

            return levelManager.Level;
        }

        [Test]
        public void WhenRaiseLevelIsCalledThenLevelManagerThrowsLevelUpEventWithCorrectOldLevel()
        {
            LevelManager levelManager = new LevelManager(0);

            levelManager.LevelUp += (oldLevel, newLevel) => {
                Assert.AreEqual(0, oldLevel);
                Assert.Pass();
            };

            levelManager.RaiseLevel();

            Assert.Fail();
        }

        [Test]
        public void WhenRaiseLevelIsCalledThenLevelmanagerThrowsLevelUpEventWithCorrectNewLevel()
        {
            LevelManager levelManager = new LevelManager(0);

            levelManager.LevelUp += (oldLevel, newLevel) => {
                Assert.AreEqual(1, newLevel);
                Assert.Pass();
            };

            levelManager.RaiseLevel();

            Assert.Fail();
        }
    }
}
