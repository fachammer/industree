using System;
using Industree.Facade;

namespace Industree.Logic
{
    public interface IActionInvoker
    {
        event Action<IPlayer, IAction, float> ActionFailure;
        event Action<IPlayer, IAction, float> ActionSuccess;
    }
}
