using UnityEngine;
using System.Collections;
using Industree.Rendering;
using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class WinLoseView : IView
    {
        private IGame game;
        private IGuiRenderer gui;

        public WinLoseView(IGame game, IGuiRenderer gui)
        {
            this.game = game;
            this.gui = gui;
        }

        public void Draw()
        {
            if (game.HasGameEnded)
            {
                if (game.PlayerWonGame)
                    gui.DrawTexture(game.WinTexture, game.ScreenBounds);
                else
                    gui.DrawTexture(game.LoseTexture, game.ScreenBounds);
            }
        }
    }
}
