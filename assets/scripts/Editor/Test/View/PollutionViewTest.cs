using NUnit.Framework;
using NSubstitute;
using Industree.Facade;
using Industree.Graphics;
using UnityEngine;
using NSubstitute.Core;

namespace Industree.View.Test
{
    public class PollutionViewTest
    {
        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawAirTexture()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            planet.AirTexture.Returns(Substitute.For<Texture>());
            planet.PollutionViewBounds.Returns(new Rect(0, 0, 1, 1));
            planet.MaximumPollution.Returns(10);
            planet.CurrentPollution.Returns(5);
            
            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            PollutionView pollutionView = new PollutionView(planet, gui, Substitute.For<IViewSkin>());           

            pollutionView.Draw();

            gui.Received().DrawTexture(planet.AirTexture, new Rect(0, 0, 0.5f, 1), 0);
        }

        [Test]
        public void WhenDrawIsCalledThenGuiRendererRecievesCallToDrawPollutionTexture()
        {
            IPlanet planet = Substitute.For<IPlanet>();
            planet.PollutionTexture.Returns(Substitute.For<Texture>());
            planet.PollutionViewBounds.Returns(new Rect(0, 0, 1, 1));
            planet.MaximumPollution.Returns(10);
            planet.CurrentPollution.Returns(5);

            IGuiRenderer gui = Substitute.For<IGuiRenderer>();
            PollutionView pollutionView = new PollutionView(planet, gui, Substitute.For<IViewSkin>());

            pollutionView.Draw();

            gui.Received().DrawTexture(planet.PollutionTexture, new Rect(0, 0, 1, 1), 1);
        }
    }
}
