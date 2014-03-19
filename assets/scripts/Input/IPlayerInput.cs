using System;
using Industree.Facade;

namespace Industree.Input
{
    public interface IPlayerInput
    {
        event Action<IPlayer, float> PlayerActionInput;
        event Action<IPlayer, float> PlayerActionSelectInput;
    }
}
