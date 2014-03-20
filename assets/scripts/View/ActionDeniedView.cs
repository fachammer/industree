using Industree.Facade;
using Industree.Graphics;
using Industree.Time;
using System;

namespace Industree.View
{
    public class ActionDeniedView : IView
    {
        private IPlayer player;
        private IClock clock;
        private IGuiRenderer gui;
        private Action drawCall;

        public ActionDeniedView(IPlayer player, IClock clock, IGuiRenderer gui)
        {
            this.player = player;
            this.clock = clock;
            this.gui = gui;
            drawCall = null;

            player.ActionFailure += AddDrawCall;
        }

        private void AddDrawCall(IPlayer player, IAction action, float actionDirection)
        {
            drawCall = () => gui.DrawTexture(action.DeniedOverlayIcon, action.IconBounds);

            clock.ClearCallbacks();
            clock.CallbackOnce(
                action.DeniedOverlayIconTime, 
                () => drawCall = null);
        }

        public void Draw()
        {
            if(drawCall != null)
                drawCall();
        }
    }
}