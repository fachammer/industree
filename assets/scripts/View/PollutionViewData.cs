using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.View
{
    [System.Serializable]
    public class PollutionViewData : ViewData
    {
        public Rect pollutionBilanceRectangle;
        public Color airColor;
        public Color pollutionColor;
        public Rect pollutionDecorationRectangle;
        public Texture pollutionDecoration;
    }
}
