using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Miscellaneous
{
    public static class SpaceConverter
    {
        public static Rect ConvertToPixelSpace(Rect originalRectangle)
        {
            return new Rect(originalRectangle.x * Screen.width, originalRectangle.y * Screen.height, originalRectangle.width * Screen.width, originalRectangle.height * Screen.height);
        }

        public static Vector2 ConvertToPixelSpace(Vector2 originalVector)
        {
            return new Vector2(originalVector.x * Screen.width, originalVector.y * Screen.height);
        }

        public static Rect ConvertToDecimalSpace(Rect originalRectangle)
        {
            return new Rect(originalRectangle.x / Screen.width, originalRectangle.y / Screen.height, originalRectangle.width / Screen.width, originalRectangle.height / Screen.height);
        }

        public static Vector2 ConvertToDecimalSpace(Vector2 originalVector)
        {
            return new Vector2(originalVector.x / Screen.width, originalVector.y / Screen.height);
        }
    }
}
