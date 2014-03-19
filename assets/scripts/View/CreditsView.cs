using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;

namespace Industree.View
{
    public class CreditsView : IView
    {
        private IPlayer player;
        private ICreditsViewData data;
        private IGuiRenderer gui;

        public CreditsView(IPlayer player, ICreditsViewData data, IGuiRenderer gui)
        {
            this.player = player;
            this.data = data;
            this.gui = gui;
        }

        public void Draw()
        {
            gui.DrawText(player.Credits.ToString(), data.TextBounds);
            gui.DrawTexture(data.Icon, data.IconBounds);
        }
    }
}
