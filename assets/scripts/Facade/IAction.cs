using Industree.Data.View;
using Industree.Facade;
using Industree.Graphics;
using UnityEngine;

namespace Industree.Facade
{
    public interface IAction
    {
        int Index { get; }
        int Cost { get; }
        float Cooldown { get; }
        float RemainingCooldown { get; }
        Rect IconBounds { get; }
        ITexture Icon { get; }
        ITexture CooldownOverlayIcon { get; }
        ITexture DeniedOverlayIcon { get; }
        ITexture SelectedOverlayIcon { get; }
        float DeniedOverlayIconTime { get; }
        void Invoke(IPlayer player, float actionDirection);
        bool IsCoolingDown { get; }
        bool IsInvokable(IPlayer player, float actionDirection);
    }
}
