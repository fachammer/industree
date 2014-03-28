using UnityEngine;
using System;

namespace Industree.Data.View
{
    public interface IActionCooldownViewData
    {
        Texture IconOverlay { get; }
        BarDecreaseDirection CooldownOverlayDecreaseDirection { get; }
    }

    public enum BarDecreaseDirection
    {
        LeftToRight,
        RightToLeft
    }

    [Serializable]
    public class ActionCooldownViewData : ViewData, IActionCooldownViewData
    {
        public Texture cooldownOverlay;
        public BarDecreaseDirection barDecreaseDirection;

        public Texture IconOverlay { get { return cooldownOverlay; } }
        public BarDecreaseDirection CooldownOverlayDecreaseDirection { get { return barDecreaseDirection; } }
    }
}
