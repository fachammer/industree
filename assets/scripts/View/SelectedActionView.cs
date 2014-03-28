using System;
using Industree.Graphics;
using Industree.Facade;

namespace Industree.View
{
    public class SelectedActionView : AbstractView
    {
        private IPlayer player;

        public SelectedActionView(IPlayer player, IGuiRenderer gui, IViewSkin skin)
            : base(gui, skin)
        {
            this.player = player;
        }

        public override void Draw()
        {
            gui.DrawTexture(player.SelectedOverlayIcon, player.SelectedAction.IconBounds);
        }
    }
}
