using assets.scripts.Miscellaneous;
using assets.scripts.Rendering;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace assets.scripts.View
{
    public class PollutionView : MonoBehaviour
    {
        public Rect pollutionBilanceRectangle;
        public Color airColor;
        public Color pollutionColor;
        public Rect pollutionDecorationRectangle;
        public Texture pollutionDecoration;

        private Pollutable pollutable;
        private Texture airTexture;
        private Texture pollutionTexture;
        private Rect airRectangle;
        private Rect pollutionRectangle;

        private void Awake()
        {
            pollutable = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Pollutable>();

            
        }

        private void Update()
        {
            float pollution = Mathf.Clamp(pollutable.currentPollution, 0, pollutable.maxPollution);

            airRectangle = new Rect(pollutionBilanceRectangle);
            pollutionRectangle = new Rect(pollutionBilanceRectangle);
            airRectangle.width = pollutionBilanceRectangle.width * (1 - pollution / pollutable.maxPollution);

            airTexture = Utilities.MakeTexture2DWithColor(airColor);
            pollutionTexture = Utilities.MakeTexture2DWithColor(pollutionColor);
        }

        private void OnGUI()
        { 
            ResolutionIndependentRenderer.DrawTexture(pollutionRectangle, pollutionTexture);
            ResolutionIndependentRenderer.DrawTexture(airRectangle, airTexture);
            ResolutionIndependentRenderer.DrawTexture(pollutionDecorationRectangle, pollutionDecoration); 
        }
    }
}
