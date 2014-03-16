using UnityEngine;
using System.Collections;
using assets.scripts.Rendering;

namespace assets.scripts.View
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
