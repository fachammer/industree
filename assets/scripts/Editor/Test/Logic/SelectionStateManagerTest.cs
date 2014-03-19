using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

namespace Industree.Logic.Test
{
    public class SelectionStateManagerTest
    {
        [Test]
        [TestCase(0, 0, ExpectedException = typeof(System.ArgumentException))]
        [TestCase(1, 0, Result = 0)]
        [TestCase(2, 0, Result = 1)]
        [TestCase(2, 1, Result = 0)]
        [TestCase(3, 0, Result = 1)]
        [TestCase(3, 1, Result = 2)]
        [TestCase(3, 2, Result = 0)]
        public int SelectNextActionTest(int numberOfActions, int selectedActionIndex)
        {
            SelectionStateManager controller = new SelectionStateManager(numberOfActions, selectedActionIndex);

            controller.SelectNextAction();

            return controller.SelectedActionIndex;
        }

        [Test]
        [TestCase(0, 0, ExpectedException=typeof(System.ArgumentException))]
        [TestCase(1, 0, Result=0)]
        [TestCase(2, 0, Result=1)]
        [TestCase(2, 1, Result=0)]
        [TestCase(3, 0, Result=2)]
        [TestCase(3, 1, Result=0)]
        [TestCase(3, 2, Result=1)]
        public int SelectPreviousActionTest(int numberOfActions, int selectedActionIndex)
        {
            SelectionStateManager controller = new SelectionStateManager(numberOfActions, selectedActionIndex);

            controller.SelectPreviousAction();

            return controller.SelectedActionIndex;
        }
    }
}