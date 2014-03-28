using System;
using UnityEngine;

namespace Industree.Data.View
{
    public interface IActionDeniedViewData
    {
        Texture IconOverlay { get; }
        float OverlayTime { get; }
    }

    [Serializable]
    public class ActionDeniedViewData : ViewData, IActionDeniedViewData
    {
        public Texture deniedOverlay;
        public float overlayTime;

        public Texture IconOverlay { get { return deniedOverlay; } }

        public float OverlayTime { get { return overlayTime; } }
    }
}
