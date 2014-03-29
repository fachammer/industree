using System;
using UnityEngine;

namespace Industree.Graphics.Internal
{
    internal class UnityGuiRenderer : IGuiRenderer
    {
        public void DrawText(string text, Rect bounds, GUIStyle style)
        {
            if (text == null)
                throw new NullReferenceException("text must not be null");

            GUI.Label(bounds, text, style);
        }

        public void DrawTexture(Texture texture, Rect bounds)
        {
            DrawTexture(texture, bounds, 0);
        }

        public void DrawTexture(Texture texture, Rect bounds, int depth)
        {
            if (texture == null)
                throw new NullReferenceException("texture must not be null");

            GUI.depth = depth;
            GUI.DrawTexture(bounds, texture, ScaleMode.StretchToFill);
        }
    }
}
