using System;
using Industree.Graphics;
using Industree.Facade;

namespace Industree.View
{
    public class SelectedActionView : IView
    {
        private IPlayer player;
        private IGuiRenderer gui;

        public SelectedActionView(IPlayer player, IGuiRenderer gui)
        {
            this.player = player;
            this.gui = gui;
        }

        public void Draw()
        {
            gui.DrawTexture(player.SelectedAction.SelectedOverlayIcon, player.SelectedAction.IconBounds);
        }
    }
}
