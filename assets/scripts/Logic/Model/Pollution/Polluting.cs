using UnityEngine;
using System;
using System.Collections;
using Industree.Time;
using Industree.Time.Internal;
using Industree.Facade;

public class Polluting : MonoBehaviour 
{
    public int pollution;
    public float timeBetweenPollution;

    private IPlanet planet;

	private void Awake() 
    {
        IPlanet planet = PlanetFactory.GetPlanet();
        Timing.GetTimerFactory().GetTimer(timeBetweenPollution).Tick += OnPollutionTimerTick;
	}

    private void OnPollutionTimerTick(ITimer timer)
    {
        if (pollution > 0)
            planet.IncreasePollution(pollution);
        else
            planet.DecreasePollution(pollution);
    }
}
