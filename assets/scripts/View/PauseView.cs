using UnityEngine;
using System.Collections;
using Industree.Rendering;
using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class PauseView : AbstractView
    {
        private IGame game;

        public PauseView(IGame game, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.game = game;
        }

        public override void Draw()
        {
            if (game.IsGamePaused && !game.HasGameEnded)
            {
                gui.DrawTexture(game.PauseTexture, game.ScreenBounds);
            }
        }
    }
}
