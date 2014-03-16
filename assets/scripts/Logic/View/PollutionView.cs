using assets.scripts.Miscellaneous;
using assets.scripts.Rendering;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace assets.scripts.View
{
    public class PollutionView : View<PollutionViewData>
    {
        private Pollutable pollutable;
        private Texture airTexture;
        private Texture pollutionTexture;
        private Rect airRectangle;
        private Rect pollutionRectangle;

        private void Awake()
        {
            pollutable = Pollutable.Get();
        }

        private void Update()
        {
            float pollution = Mathf.Clamp(pollutable.currentPollution, 0, pollutable.maxPollution);

            airRectangle = new Rect(data.pollutionBilanceRectangle);
            pollutionRectangle = new Rect(data.pollutionBilanceRectangle);
            airRectangle.width = data.pollutionBilanceRectangle.width * (1 - pollution / pollutable.maxPollution);

            airTexture = Utilities.MakeTexture2DWithColor(data.airColor);
            pollutionTexture = Utilities.MakeTexture2DWithColor(data.pollutionColor);
        }

        protected override void Draw()
        { 
            ResolutionIndependentRenderer.DrawTexture(pollutionRectangle, pollutionTexture);
            ResolutionIndependentRenderer.DrawTexture(airRectangle, airTexture);
            ResolutionIndependentRenderer.DrawTexture(data.pollutionDecorationRectangle, data.pollutionDecoration); 
        }
    }
}
