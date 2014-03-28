using System;
using UnityEngine;

namespace Industree.Facade
{
    public interface IPlanet
    {
        Texture PollutionTexture { get; }
        Texture AirTexture { get; }
        Rect PollutionViewBounds { get; }
        int MaximumPollution { get; }
        int CurrentPollution { get; }

        event Action MaximumPollutionReached;
        event Action ZeroPollutionReached;
    }
}
