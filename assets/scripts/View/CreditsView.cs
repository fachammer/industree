using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;

namespace Industree.View
{
    public class CreditsView : AbstractView
    {
        private IPlayer player;

        public CreditsView(IPlayer player, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.player = player;
        }

        public override void Draw()
        {
            gui.DrawText(player.Credits.ToString(), player.CreditsTextBounds, skin.Label);
            gui.DrawTexture(player.CreditsIcon, player.CreditsIconBounds);
        }
    }
}
