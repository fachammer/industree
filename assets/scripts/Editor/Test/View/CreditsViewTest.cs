using NUnit.Framework;
using NSubstitute;
using Industree.View;
using Industree.Facade;
using Industree.Data.View;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View.Test
{
    public class CreditsViewTest
    {
        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawText()
        {
            IPlayer player = Substitute.For<IPlayer>();
            int playerCredits = 1;
            player.Credits.Returns(playerCredits);
            player.CreditsTextBounds.Returns(new Rect());

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            IViewSkin skin = Substitute.For<IViewSkin>();
            CreditsView creditsView = new CreditsView(player, gui, skin);

            creditsView.Draw();

            gui.Received().DrawText(playerCredits.ToString(), player.CreditsTextBounds, skin.Label);
        }

        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawTexture()
        {
            IPlayer player = Substitute.For<IPlayer>();
            int playerCredits = 1;
            player.Credits.Returns(playerCredits);
            player.CreditsIconBounds.Returns(new Rect());
            player.CreditsIcon.Returns(Substitute.For<Texture>());

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();

            CreditsView creditsView = new CreditsView(player, gui, Substitute.For<IViewSkin>());

            creditsView.Draw();

            gui.Received().DrawTexture(player.CreditsIcon, player.CreditsIconBounds);
        }
    }
}
