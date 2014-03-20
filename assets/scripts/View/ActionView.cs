using Industree.Data.View;
using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class ActionView : IView
    {
        private IAction action;
        private IActionViewData actionViewData;
        private IGuiRenderer gui;

        public ActionView(IAction action, IActionViewData actionViewData, IGuiRenderer gui)
        {
            this.action = action;
            this.actionViewData = actionViewData;
            this.gui = gui;
        }

        public void Draw()
        {
            gui.DrawTexture(actionViewData.Icon, actionViewData.IconBounds);
            gui.DrawText(action.Cost.ToString(), actionViewData.CostBounds);
        }
    }
}
