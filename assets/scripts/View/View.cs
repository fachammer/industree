using Industree.Miscellaneous;
using UnityEngine;

namespace Industree.View
{
    public abstract class View<T> : DataSubject<T> where T : ViewData
    {
        protected void OnGUI()
        {
            GUI.skin = data.guiSkin;
            GUI.depth = data.guiDepth;
            Draw();
        }

        protected abstract void Draw();
    }
}
