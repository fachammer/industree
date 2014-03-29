using System;
using UnityEngine;

namespace Industree.Facade
{
    public interface IPlayer
    {
        event Action<IPlayer, float> ActionInput;
        event Action<IPlayer, IAction, float> ActionSuccess;
        event Action<IPlayer, IAction, float> ActionFailure;
        event Action<int, int> CreditsChange;

        IAction[] Actions { get; }
        IAction SelectedAction { get; }
        int Credits { get; }
        int Index { get; }
        void SelectNextAction();
        void SelectPreviousAction();
        void IncreaseCredits(int amount);
        void DecreaseCredits(int amount);

        Texture CreditsIcon { get; }
        Rect CreditsIconBounds { get; }
        Rect CreditsTextBounds { get; }
        Texture SelectedOverlayIcon { get; }
    }
}
