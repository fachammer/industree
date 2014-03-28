using Industree.Facade;
using Industree.Graphics;
using Industree.Time;

namespace Industree.View
{
    public class ActionView : IView
    {
        private ActionIconView actionIconView;
        private ActionCooldownView actionCooldownView;
        private ActionDeniedView actionDeniedView;

        public ActionView(IAction action, IGuiRenderer gui, IViewSkin skin)
        {
            actionIconView = new ActionIconView(action, gui, skin);
            actionCooldownView = new ActionCooldownView(action, gui, skin);
            actionDeniedView = new ActionDeniedView(action, Timing.GetTimerFactory(), gui, skin);
        }

        public void Draw()
        {
            actionIconView.Draw();
            actionCooldownView.Draw();
            actionDeniedView.Draw();
        }
    }
}
