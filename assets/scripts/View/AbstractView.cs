using Industree.Graphics;
using UnityEngine;

namespace Industree.View
{
    public abstract class AbstractView : IView
    {
        protected readonly IGuiRenderer gui;
        protected readonly IViewSkin skin;

        protected AbstractView(IGuiRenderer gui, IViewSkin skin)
        {
            this.gui = gui;
            this.skin = skin;
        }

        public abstract void Draw();
    }
}
