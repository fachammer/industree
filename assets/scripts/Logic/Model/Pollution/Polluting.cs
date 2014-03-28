using UnityEngine;
using System;
using System.Collections;
using Industree.Time;
using Industree.Time.Internal;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;

    private Pollutable[] pollutables;

	private void Awake() {
		pollutables = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Pollutable)), item => (Pollutable)item);
        Timing.GetTimerFactory().GetTimer(timeBetweenPollution).Tick += OnPollutionTimerTick;
	}

    private void OnPollutionTimerTick(ITimer timer)
    {
    	foreach(Pollutable pollutable in pollutables){
    		pollutable.IncreasePollution(pollution);
    	}
        
    }
}
