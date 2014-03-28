using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using System;

namespace Industree.Input.Internal.Test
{
    public class PlayerInputTest
    {
        [Test]
        public void WhenSelectAxisChangesAndNewValueIsPositiveThenPlayerInputThrowsPlayerActionSelectInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionSelectInput += (oldValue, newValue) => {
                Assert.Pass();
            };

            selectAxis.Change += Raise.Event<Action<float, float>>(0f, 1f);

            Assert.Fail();
        }

        [Test]
        public void WhenSelectAxisChangesAndNewValueIsNegativeThenPlayerInputThrowsPlayerActionSelectInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionSelectInput += (oldValue, newValue) => {
                Assert.Pass();
            };

            selectAxis.Change += Raise.Event<Action<float, float>>(0f, -1f);

            Assert.Fail();
        }

        [Test]
        public void WhenSelectAxisChangesAndValueIsZeroThenPlayerInputDoesNotThrowPlayerActionSelectInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionSelectInput += (oldValue, newValue) => {
                Assert.Fail();
            };

            selectAxis.Change += Raise.Event<Action<float, float>>(1f, 0f);

            Assert.Pass();
        }

        [Test]
        public void WhenActionAxisChangesAndNewValueIsPositiveThenPlayerInputThrowsPlayerActionInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionInput += (oldValue, newValue) => {
                Assert.Pass();
            };

            actionAxis.Change += Raise.Event<Action<float, float>>(0f, 1f);

            Assert.Fail();
        }

        [Test]
        public void WhenActionAxisChangesAndNewValueIsNegativeThenPlayerInputThrowsPlayerActionInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionInput += (oldValue, newValue) => {
                Assert.Pass();
            };

            actionAxis.Change += Raise.Event<Action<float, float>>(0f, -1f);

            Assert.Fail();
        }

        [Test]
        public void WhenActionAxisChangesAndValueIsZeroThenPlayerInputDoesNotThrowPlayerActionInputEvent()
        {
            IAxis selectAxis = Substitute.For<IAxis>();
            IAxis actionAxis = Substitute.For<IAxis>();
            IPlayer player = Substitute.For<IPlayer>();
            PlayerInput playerInput = new PlayerInput(player, selectAxis, actionAxis);

            playerInput.PlayerActionInput += (oldValue, newValue) => {
                Assert.Fail();
            };

            actionAxis.Change += Raise.Event<Action<float, float>>(1f, 0f);

            Assert.Pass();
        }
    }
}
