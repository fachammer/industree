using Industree.Facade;
using UnityEngine;

namespace Industree.Facade
{
    public interface IAction
    {
        int Index { get; }
        int Cost { get; }
        float Cooldown { get; }
        Rect IconBounds { get; }
        float GetRemainingCooldown();
        void Invoke(IPlayer player, float actionDirection);
        bool IsCoolingDown { get; }
        bool IsInvokable(IPlayer player, float actionDirection);
    }
}
