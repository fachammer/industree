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

            ICreditsViewData data = Substitute.For<ICreditsViewData>();
            Rect fakeRect = new Rect();
            data.TextBounds.Returns(fakeRect);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();

            CreditsView creditsView = new CreditsView(player, data, gui);

            creditsView.Draw();

            gui.Received().DrawText(playerCredits.ToString(), fakeRect);
        }

        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawTexture()
        {
            IPlayer player = Substitute.For<IPlayer>();
            int playerCredits = 1;
            player.Credits.Returns(playerCredits);

            ICreditsViewData data = Substitute.For<ICreditsViewData>();
            Rect fakeRect = new Rect();
            data.IconBounds.Returns(fakeRect);
            ITexture fakeTexture = Substitute.For<ITexture>();
            data.Icon.Returns(fakeTexture);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();

            CreditsView creditsView = new CreditsView(player, data, gui);

            creditsView.Draw();

            gui.Received().DrawTexture(fakeTexture, fakeRect);
        }
    }
}
