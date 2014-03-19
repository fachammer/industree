using System;
using Industree.Facade;
using Industree.Miscellaneous;

namespace Industree.Input.Internal
{
    public class PlayerInput : IPlayerInput
    {
        public string selectInputName;
        public string actionInputName;

        private IPlayer player;
        private float previousSelectInputAxis;
        private float previousActionInputAxis;

        public event Action<IPlayer, float> PlayerActionSelectInput = (player, selectDirection) => { };
        public event Action<IPlayer, float> PlayerActionInput = (player, actionDirection) => { };

        public PlayerInput(IPlayer player)
        {
            this.player = player;
        }

        private void Update()
        {
            HandleAxis(selectInputName, ref previousSelectInputAxis, PlayerActionSelectInput);
            HandleAxis(actionInputName, ref previousActionInputAxis, PlayerActionInput);
        }

        private void HandleAxis(string axisName, ref float previousAxisValue, Delegate inputEvent)
        {
            float rawAxisValue;
            float axisValue = Utilities.GetAxisRawDown(axisName, previousAxisValue, out rawAxisValue);

            if (axisValue != 0)
            {
                inputEvent.DynamicInvoke(player, axisValue);
            }

            previousAxisValue = rawAxisValue;
        }
    }
}