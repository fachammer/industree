using UnityEngine;

namespace Industree.Graphics.Internal
{
    internal class ResolutionIndependentGuiRenderer : IGuiRenderer
    {
        private IScreen screen;
        private IGuiRenderer innerRenderer;

        public ResolutionIndependentGuiRenderer(IScreen screen, IGuiRenderer innerRenderer)
        {
            this.screen = screen;
            this.innerRenderer = innerRenderer;
        }

        public void DrawText(string text, Rect bounds, GUIStyle style)
        {
            innerRenderer.DrawText(text, CalculateActualBounds(bounds), CalculateActualGUIStyle(style));
        }

        private GUIStyle CalculateActualGUIStyle(GUIStyle originalStyle)
        {
            int fontSize = CalculateActualFontSize(originalStyle);
            GUIStyle actualStyle = new GUIStyle(originalStyle);
            actualStyle.fontSize = fontSize;
            return actualStyle;
        }

        private int CalculateActualFontSize(GUIStyle originalStyle)
        {
            return (int)(originalStyle.fontSize / 100 * screen.Height * 0.75f);
        }

        public void DrawTexture(Texture texture, Rect bounds)
        {
            innerRenderer.DrawTexture(texture, CalculateActualBounds(bounds));
        }

        public void DrawTexture(Texture texture, Rect bounds, int depth)
        {
            innerRenderer.DrawTexture(texture, CalculateActualBounds(bounds), depth);
        }

        private Rect CalculateActualBounds(Rect originalBounds)
        {
            return new Rect(
                originalBounds.x * screen.Width, 
                originalBounds.y * screen.Height, 
                originalBounds.width * screen.Width, 
                originalBounds.height * screen.Height);
        }
    }
}
