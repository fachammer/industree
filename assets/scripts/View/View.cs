using assets.scripts.Miscellaneous;
using UnityEngine;

namespace assets.scripts.View
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
