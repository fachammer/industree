using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View.Test
{
    public class WinLoseViewTest
    {
        [Test]
        public void GivenGameHasEndedAndPlayerWonGameWhenDrawIsCalledThenGuiRendererRecievesDrawTextureCallWithWinTexture()
        {
            IGame game = Substitute.For<IGame>();
            game.HasGameEnded.Returns(true);
            game.PlayerWonGame.Returns(true);
            game.WinTexture.Returns(Substitute.For<Texture>());
            game.ScreenBounds.Returns(new Rect(0, 0, 1, 1));
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            WinLoseView winLoseView = new WinLoseView(game, gui, Substitute.For<IViewSkin>());

            winLoseView.Draw();

            gui.Received().DrawTexture(game.WinTexture, game.ScreenBounds);
        }

        [Test]
        public void GivenGameHasEndedAndPlayerDidNotWinGameWhenDrawIsCalledThenGuiRendererRecievesDrawTextureCallWithLoseTexture()
        {
            IGame game = Substitute.For<IGame>();
            game.HasGameEnded.Returns(true);
            game.PlayerWonGame.Returns(false);
            game.LoseTexture.Returns(Substitute.For<Texture>());
            game.ScreenBounds.Returns(new Rect(0, 0, 1, 1));
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            WinLoseView winLoseView = new WinLoseView(game, gui, Substitute.For<IViewSkin>());

            winLoseView.Draw();

            gui.Received().DrawTexture(game.LoseTexture, game.ScreenBounds);
        }

        [Test]
        public void GivenGameHasNotEndedWhenDrawIsCalledThenGuiRendererDoesNotRecieveDrawTextureCall()
        {
            IGame game = Substitute.For<IGame>();
            game.HasGameEnded.Returns(false);
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            WinLoseView winLoseView = new WinLoseView(game, gui, Substitute.For<IViewSkin>());

            winLoseView.Draw();

            gui.DidNotReceiveWithAnyArgs().DrawTexture(Substitute.For<Texture>(), new Rect());
        }
    }
}
