using System;
using UnityEngine;

namespace Industree.Data.View
{
    [Serializable]
    public class WinLoseViewData : ViewData
    {
        public Rect dialogRectangle;
        public Texture2D winDialog;
        public Texture2D loseDialog;
    }
}
