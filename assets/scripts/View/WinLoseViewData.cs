using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.View
{
    [System.Serializable]
    public class WinLoseViewData : ViewData
    {
        public Rect dialogRectangle;
        public Texture2D winDialog;
        public Texture2D loseDialog;
    }
}
