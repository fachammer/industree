using UnityEngine;
using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;

namespace Industree.View
{
    public class ActionCooldownView : AbstractView
    {
        private IAction action;

        public ActionCooldownView(IAction action, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.action = action;
        }

        public override void Draw()
        {
            if(action.IsCoolingDown)
                gui.DrawTexture(action.CooldownOverlayIcon, GetOverlayBounds());
        }

        private Rect GetOverlayBounds()
        {
            if (action.CooldownDecreaseDirection == BarDecreaseDirection.LeftToRight)
                return GetLeftToRightOverlayBounds();
            else
                return GetRightToLeftOverlayBounds();
            
        }

        private Rect GetLeftToRightOverlayBounds()
        {
            Rect overlayBounds = new Rect(GetRightToLeftOverlayBounds());
            overlayBounds.x = action.IconBounds.x + (action.IconBounds.width - overlayBounds.width);
            return overlayBounds;
        }

        private Rect GetRightToLeftOverlayBounds()
        {
            Rect overlayBounds = new Rect(action.IconBounds);
            overlayBounds.width = action.IconBounds.width * action.RemainingCooldown / action.Cooldown;
            return overlayBounds;
        }
    }
}
