using UnityEngine;

namespace Industree.Graphics
{
    public interface IGuiRenderer
    {
        void DrawText(string text, Rect bounds, GUIStyle style);
        void DrawTexture(Texture texture, Rect bounds);
        void DrawTexture(Texture texture, Rect bounds, int depth);
    }
}
