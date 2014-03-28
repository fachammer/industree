using Industree.Data.View;
using Industree.Facade;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View
{
    public class ActionIconView : AbstractView
    {
        private IAction action;

        public ActionIconView(IAction action, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.action = action;
        }

        public override void Draw()
        {
            gui.DrawTexture(action.Icon, action.IconBounds);
            gui.DrawText(action.Cost.ToString(), action.CostBounds, skin.Label);
        }
    }
}
