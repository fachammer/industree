using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Rendering
{
    public static class ResolutionIndependentRenderer
    {
        public static void DrawTexture(Rect rectangle, Texture texture)
        {
            GUI.DrawTexture(CalculateRealRectangle(rectangle), texture, ScaleMode.StretchToFill);
        }

        public static void Label(Rect rectangle, string text, GUIStyle style)
        {
            GUIStyle guiStyle = new GUIStyle(style);
            guiStyle.fontSize = (int)(guiStyle.fontSize / 100f * Screen.height * 20f / 26f);
            GUI.Label(CalculateRealRectangle(rectangle), text, guiStyle);
        }

        public static bool Button(Rect rectangle, GUIContent guiContent, GUIStyle style)
        {
            return GUI.Button(CalculateRealRectangle(rectangle), guiContent, style);
        }

        private static Rect CalculateRealRectangle(Rect resolutionIndependentRectangle)
        {
            return new Rect(
                resolutionIndependentRectangle.x * Screen.width,
                resolutionIndependentRectangle.y * Screen.height,
                resolutionIndependentRectangle.width * Screen.width,
                resolutionIndependentRectangle.height * Screen.height);
        }
    }
}
