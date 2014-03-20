using UnityEngine;
using System.Collections;
using Industree.Rendering;
using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class PauseView : IView
    {
        private IGame game;
        private IGuiRenderer gui;

        public PauseView(IGame game, IGuiRenderer gui)
        {
            this.game = game;
            this.gui = gui;
        }

        public void Draw()
        {
            if (game.IsGamePaused && !game.HasGameEnded)
            {
                gui.DrawTexture(game.PauseTexture, game.ScreenBounds);
            }
        }
    }
}
