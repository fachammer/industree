using Industree.Data.View;
using Industree.Facade;
using System;
using UnityEngine;

namespace Industree.Facade
{
    public interface IAction
    {
        event Action<IPlayer, IAction, float> Failure;

        int Index { get; }
        int Cost { get; }
        float Cooldown { get; }
        float RemainingCooldown { get; }
        Rect IconBounds { get; }
        Rect CostBounds { get; }
        Texture Icon { get; }
        Texture CooldownOverlayIcon { get; }
        Texture DeniedOverlayIcon { get; }
        float DeniedOverlayIconTime { get; }
        void Invoke(IPlayer player, float actionDirection);
        void Fail(IPlayer player, float actionDirection);
        bool IsCoolingDown { get; }
        bool IsInvokable(IPlayer player, float actionDirection);
    }
}
