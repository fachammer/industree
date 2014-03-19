using System;
using Industree.Data.View;
using Industree.Graphics;
using Industree.Facade;

namespace Industree.View
{
    public class SelectedActionView : IView
    {
        private IPlayer player;
        private ISelectedActionViewData data;
        private IGuiRenderer gui;

        public SelectedActionView(IPlayer player, ISelectedActionViewData data, IGuiRenderer gui)
        {
            this.player = player;
            this.data = data;
            this.gui = gui;
        }

        public void Draw()
        {
            gui.DrawTexture(data.IconOverlay, player.SelectedAction.IconBounds);
        }
    }
}
