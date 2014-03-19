using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Industree.Graphics
{
    public interface IGuiRenderer
    {
        void DrawText(string text, Rect bounds);
        void DrawTexture(ITexture texture, Rect bounds);
    }
}
