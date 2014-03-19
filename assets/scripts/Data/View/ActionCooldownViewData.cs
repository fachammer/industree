using UnityEngine;
using System.Collections;
using Industree.Rendering;

namespace Industree.View
{
    public enum BarDecreaseDirection
    {
        LeftToRight,
        RightToLeft
    }

    [System.Serializable]
    public class ActionCooldownViewData : ViewData
    {
        public Texture cooldownOverlay;
        public BarDecreaseDirection barDecreaseDirection;
    }
}
