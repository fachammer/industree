using UnityEngine;
using System;

namespace Industree.Facade.Internal
{
    internal class Planet : MonoBehaviour, IPlanet
    {
        public event System.Action MaximumPollutionReached;

        public event System.Action ZeroPollutionReached;

        public Texture PollutionTexture
        {
            get { throw new NotImplementedException(); }
        }   

        public Texture AirTexture
        {
            get { throw new NotImplementedException(); }
        }

        public Rect PollutionViewBounds
        {
            get { throw new NotImplementedException(); }
        }

        public int MaximumPollution
        {
            get { throw new NotImplementedException(); }
        }

        public int CurrentPollution
        {
            get { throw new NotImplementedException(); }
        }
    }
}
