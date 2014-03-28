using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class PlayerView : IView
    {
        private SelectedActionView selectedActionView;
        private CreditsView creditsView;

        public PlayerView(IPlayer player, IGuiRenderer gui, IViewSkin skin)
        {
            selectedActionView = new SelectedActionView(player, gui, skin);
            creditsView = new CreditsView(player, gui, skin);
        }

        public void Draw()
        {
            selectedActionView.Draw();
            creditsView.Draw();
            
        }
    }
}
