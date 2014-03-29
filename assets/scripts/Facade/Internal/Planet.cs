using UnityEngine;
using System;
using Industree.Data.View;
using Industree.Graphics;
using Industree.View;
using Industree.Logic.StateManager;

namespace Industree.Facade.Internal
{
    internal class Planet : MonoBehaviour, IPlanet
    {
        public int maximumPollution = 1;
        public int currentPollution = 0;
        public PollutionViewData pollutionViewData = null;

        private PollutionManager pollutionManager;

        private PollutionView pollutionView;

        public event System.Action MaximumPollutionReached = () => { };
        public event System.Action ZeroPollutionReached = () => { };

        public Texture PollutionTexture { get { return GraphicsUtility.CreateTextureWithColor(pollutionViewData.pollutionColor); } }
        public Texture AirTexture { get { return GraphicsUtility.CreateTextureWithColor(pollutionViewData.airColor); } }
        public Rect PollutionViewBounds { get { return pollutionViewData.pollutionBilanceRectangle; } }
        public int MaximumPollution { get { return pollutionManager.MaximumPollution; } }
        public int CurrentPollution { get { return pollutionManager.CurrentPollution; } }

        public void IncreasePollution(int pollutionAmount)
        {
            pollutionManager.IncreasePollutionByAmount(pollutionAmount);
        }

        public void DecreasePollution(int pollutionAmount)
        {
            pollutionManager.DecreasePollutionByAmount(pollutionAmount);
        }

        private void Awake()
        {
            pollutionManager = new PollutionManager(maximumPollution, currentPollution);
            pollutionView = new PollutionView(this, GuiRendererFactory.GetResolutionIndependentRenderer(), pollutionViewData.viewSkin);

            pollutionManager.MaximumPollutionReached += MaximumPollutionReached;
            pollutionManager.ZeroPollutionReached += ZeroPollutionReached;
        }

        private void OnGUI()
        {
            pollutionView.Draw();
        }
    }
}
