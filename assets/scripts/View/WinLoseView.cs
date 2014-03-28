using UnityEngine;
using System.Collections;
using Industree.Rendering;
using Industree.Facade;
using Industree.Graphics;

namespace Industree.View
{
    public class WinLoseView : AbstractView
    {
        private IGame game;

        public WinLoseView(IGame game, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.game = game;
        }

        public override void Draw()
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
