using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Inspector
{
    [Serializable]
    public class MultidimensionalRectangle
    {
        public Rect[] rectangles;

        public Rect this[int index]{
            get { return rectangles[index]; }
        }
    }
}
