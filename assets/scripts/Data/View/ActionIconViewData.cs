using System;
using UnityEngine;

namespace Industree.Data.View
{
    public interface IActionIconViewData
    {
        Texture Icon { get; }
        Rect IconBounds { get; }
        Rect CostBounds { get; }
    }

    [Serializable]
    public class ActionIconViewData : ViewData, IActionIconViewData
    {
        public Texture actionIcon;
        public Rect iconBounds;
        public Rect costBounds;

        public Texture Icon { get { return actionIcon; } }
        public Rect IconBounds { get { return iconBounds; } }
        public Rect CostBounds { get { return costBounds; } }
    }
}
