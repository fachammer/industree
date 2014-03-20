using UnityEngine;

namespace Industree.Graphics
{
    public interface IGuiRenderer
    {
        void DrawText(string text, Rect bounds);
        void DrawTexture(ITexture texture, Rect bounds);
    }
}
