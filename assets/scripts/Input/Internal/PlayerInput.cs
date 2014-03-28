using System;
using Industree.Facade;

namespace Industree.Input.Internal
{
    public class PlayerInput : IPlayerInput
    {
        private IPlayer player;

        public event Action<IPlayer, float> PlayerActionSelectInput = (player, selectDirection) => { };
        public event Action<IPlayer, float> PlayerActionInput = (player, actionDirection) => { };

        public PlayerInput(IPlayer player, IAxis selectAxis, IAxis actionAxis)
        {
            this.player = player;
            selectAxis.Change += OnSelectAxisChange;
            actionAxis.Change += OnActionAxisChange;
        }

        private void OnSelectAxisChange(float oldValue, float newValue)
        {
            if(newValue != 0f)
                PlayerActionSelectInput(player, newValue);
        }

        private void OnActionAxisChange(float oldValue, float newValue)
        {
            if(newValue != 0f)
                PlayerActionInput(player, newValue);
        }
    }
}