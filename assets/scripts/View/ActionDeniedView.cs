using Industree.Facade;
using Industree.Graphics;
using Industree.Time;
using System;
using UnityEngine;

namespace Industree.View
{
    public class ActionDeniedView : AbstractView
    {
        private Action drawCall;
        private ITimerFactory timerFactory;

        public ActionDeniedView(IAction action, ITimerFactory timerFactory, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.timerFactory = timerFactory;
            drawCall = null;

            action.Failure += AddDrawCall;
        }

        private void AddDrawCall(IPlayer player, IAction action, float actionDirection)
        {
            ITimer timer = timerFactory.GetTimer(action.DeniedOverlayIconTime);
            timer.Tick += OnActionDeniedTimerTick;
            drawCall = () => gui.DrawTexture(action.DeniedOverlayIcon, action.IconBounds);
        }

        private void OnActionDeniedTimerTick(ITimer timer)
        {
            timer.Stop();
            drawCall = null;
        }

        public override void Draw()
        {
            if (drawCall != null)
                drawCall();               
        }
    }
}