using Industree.Facade;
using NSubstitute;
using NUnit.Framework;

namespace Industree.Logic.Test
{
    public class ActionInvokerTest
    {
        [Test]
        [TestCase(0, 0, false, true, true, false)]
        [TestCase(1, 1, false, true, true, false)]
        [TestCase(1, 2, false, true, false, true)]
        [TestCase(0, 0, true, true, false, true)]
        [TestCase(0, 0, false, false, false, true)]
        [TestCase(0, 0, true, false, false, true)]
        public void ActionInvokerEventsThrownWithCorrectParametersOnInvokeTest(
            int playerCredits, 
            int actionCost, 
            bool isActionCoolingDown, 
            bool isActionInvokable, 
            bool shouldThrowActionSuccess, 
            bool shouldThrowActionFailure)
        {
            float actionDirection = 1f;
            IPlayer player = Substitute.For<IPlayer>();
            IAction action = Substitute.For<IAction>();
            player.Credits.Returns(playerCredits);
            action.Cost.Returns(actionCost);
            action.IsCoolingDown.Returns(isActionCoolingDown);
            action.IsInvokable(player, actionDirection).Returns(isActionInvokable);
            ActionInvoker actionInvoker = new ActionInvoker();

            bool successEventThrown = false;
            if (shouldThrowActionSuccess)
            {
                actionInvoker.ActionSuccess += (p, a, direction) => {
                    Assert.AreSame(player, p);
                    Assert.AreSame(action, a);
                    Assert.AreEqual(actionDirection, direction);
                    successEventThrown = true;
                };
            }

            bool failureEventThrown = false;
            if (shouldThrowActionFailure)
            {
                actionInvoker.ActionFailure += (p, a, direction) => {
                    Assert.AreSame(player, p);
                    Assert.AreSame(action, a);
                    Assert.AreEqual(actionDirection, direction);
                    failureEventThrown = true;
                };
            }

            actionInvoker.Invoke(player, action, 1f);

            if (shouldThrowActionSuccess && successEventThrown || shouldThrowActionFailure && failureEventThrown)
                Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void WhenInvokeOnActionInvokerSucceedsThenInvokeOnActionIsCalled()
        {
            float actionDirection = 1f;
            IAction action = Substitute.For<IAction>();
            IPlayer player = Substitute.For<IPlayer>();
            action.Cost.Returns(0);
            player.Credits.Returns(0);
            action.IsInvokable(player, actionDirection).Returns(true);
            action.IsCoolingDown.Returns(false);
            ActionInvoker actionInvoker = new ActionInvoker();

            actionInvoker.Invoke(player, action, actionDirection);

            action.Received().Invoke(player, actionDirection);
        }

        [Test]
        public void WhenInvokeOnActionInvokerFailsThenInvokeOnActionIsNotCalled()
        {
            float actionDirection = 1f;
            IAction action = Substitute.For<IAction>();
            IPlayer player = Substitute.For<IPlayer>();
            action.Cost.Returns(1);
            player.Credits.Returns(0);
            action.IsInvokable(player, actionDirection).Returns(true);
            action.IsCoolingDown.Returns(false);
            ActionInvoker actionInvoker = new ActionInvoker();

            actionInvoker.Invoke(player, action, actionDirection);

            action.DidNotReceive().Invoke(player, actionDirection);
        }
    }
}
