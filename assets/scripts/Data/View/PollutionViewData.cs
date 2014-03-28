using System;
using UnityEngine;

namespace Industree.Data.View
{
    [Serializable]
    public class PollutionViewData : ViewData
    {
        public Rect pollutionBilanceRectangle;
        public Color airColor;
        public Color pollutionColor;
        public Rect pollutionDecorationRectangle;
        public Texture pollutionDecoration;
    }
}
