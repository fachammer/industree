using System;

namespace Industree.Facade
{
    public interface IPlanet
    {
        event Action MaximumPollution;
        event Action ZeroPollution;
    }
}
