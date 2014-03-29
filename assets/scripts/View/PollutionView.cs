using Industree.Facade;
using Industree.Graphics;
using UnityEngine;

namespace Industree.View
{
    public class PollutionView : AbstractView
    {
        private IPlanet planet;

        public PollutionView(IPlanet planet, IGuiRenderer gui, IViewSkin skin) : base(gui, skin)
        {
            this.planet = planet;
        }

        public override void Draw()
        {
            Rect airBounds = CalculateAirBounds();
            gui.DrawTexture(planet.PollutionTexture, planet.PollutionViewBounds, 1);
            gui.DrawTexture(planet.AirTexture, airBounds, 0);
        }

        private Rect CalculateAirBounds()
        {
            Rect airBounds = new Rect(planet.PollutionViewBounds);
            airBounds.width =  (1f - ((float) planet.CurrentPollution) / planet.MaximumPollution) * planet.PollutionViewBounds.width;
            return airBounds;
        }
    }
}
