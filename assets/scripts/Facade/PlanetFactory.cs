using Industree.Facade.Internal;
using System;
using UnityEngine;

namespace Industree.Facade
{
    public class PlanetFactory
    {
        public static IPlanet GetPlanet()
        {
            return GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
        }
    }
}
