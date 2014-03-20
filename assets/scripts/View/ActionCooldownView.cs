using UnityEngine;
using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;

namespace Industree.View
{
    public class ActionCooldownView : IView
    {
        private IAction action;
        private IActionViewData actionViewData;
        private IActionCooldownViewData actionCooldownViewData;
        private IGuiRenderer gui;

        public ActionCooldownView(IAction action, IActionViewData actionViewData, IActionCooldownViewData data, IGuiRenderer gui)
        {
            this.action = action;
            this.actionViewData = actionViewData;
            this.actionCooldownViewData = data;
            this.gui = gui;
        }

        public void Draw()
        {
            if(action.IsCoolingDown)
                gui.DrawTexture(actionCooldownViewData.IconOverlay, GetOverlayBounds());
        }

        private Rect GetOverlayBounds()
        {
            Rect overlayBounds = new Rect(actionViewData.IconBounds);
            overlayBounds.width = actionViewData.IconBounds.width * action.RemainingCooldown / action.Cooldown;
            return overlayBounds;
        }
    }
}
