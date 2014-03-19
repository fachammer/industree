using Industree.Logic;
using System;

namespace Industree.Facade
{
    public interface IPlayer
    {
        IAction[] Actions { get; }
        IAction SelectedAction { get; }
        IActionInvoker ActionInvoker { get; }
        int Credits { get; }
        int Index { get; }
        void SelectNextAction();
        void SelectPreviousAction();
        void IncreaseCredits(int amount);
        event Action<IPlayer, float> ActionInput;
        event Action<IPlayer, IAction, float> ActionSuccess;
    }
}
