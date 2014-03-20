using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View.Test
{
    public class PauseViewTest
    {
        [Test]
        public void GivenPauseViewIsInstantiatedAndGameIsPausedWhenDrawIsCalledThenGuiRendererRecievesDrawTextureCall()
        {
            IGame game = Substitute.For<IGame>();
            game.IsGamePaused.Returns(true);
            game.PauseTexture.Returns(Substitute.For<ITexture>());
            game.ScreenBounds.Returns(new Rect(0, 0, 1, 1));
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            PauseView pauseView = new PauseView(game, gui);

            pauseView.Draw();

            gui.Received().DrawTexture(game.PauseTexture, game.ScreenBounds);
        }

        [Test]
        public void GivenPauseViewIsInstantiatedAndGameIsPausedAndGameHasEndedWhenDrawIsCalledThenGuiRendererRecievesNoDrawTextureCall()
        {
            IGame game = Substitute.For<IGame>();
            game.HasGameEnded.Returns(true);
            game.IsGamePaused.Returns(true);
            game.PauseTexture.Returns(Substitute.For<ITexture>());
            game.ScreenBounds.Returns(new Rect(0, 0, 1, 1));
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            PauseView pauseView = new PauseView(game, gui);

            pauseView.Draw();

            gui.DidNotReceive().DrawTexture(game.PauseTexture, game.ScreenBounds);
        }
    }
}
