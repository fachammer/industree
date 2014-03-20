using UnityEngine;
using System;

namespace Industree.Facade.Internal
{
    internal class Planet : MonoBehaviour, IPlanet
    {
        public event System.Action MaximumPollution;

        public event System.Action ZeroPollution;
    }
}
