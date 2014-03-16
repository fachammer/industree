using UnityEngine;
using System;
using System.Collections;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;

    private Pollutable[] pollutables;

	private void Awake() {
		pollutables = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Pollutable)), item => (Pollutable)item);
        Timer.Start(timeBetweenPollution, OnPollutionTimerTick);
	}

    private void OnPollutionTimerTick(Timer timer)
    {
    	foreach(Pollutable pollutable in pollutables){
    		pollutable.IncreasePollution(pollution);
    	}
        
    }
}
