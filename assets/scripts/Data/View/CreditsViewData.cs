using System;
using UnityEngine;

namespace Industree.Data.View
{
    public interface ICreditsViewData
    {
        Rect TextBounds { get; }
        Rect IconBounds { get; }
        Texture Icon { get; }
    }

    [Serializable]
    public class CreditsViewData : ViewData, ICreditsViewData
    {
        public Texture creditIcon;
        public Rect creditIconRectangle;
        public Rect creditRectangle;

        public Rect TextBounds { get { return creditRectangle; } }
        public Rect IconBounds { get { return creditIconRectangle; } }
        public Texture Icon { get { return creditIcon; } }
    }
}
