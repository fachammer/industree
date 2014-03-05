using UnityEngine;
using System;
using System.Collections;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;

    [HideInInspector]
    private Pollutable[] pollutables;

	private void Awake() {
		pollutables = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Pollutable)), item => (Pollutable)item);
        Timer.AddTimerToGameObject(gameObject, timeBetweenPollution, OnPollutionTimerTick);
	}

    private void OnPollutionTimerTick(Timer timer)
    {
    	foreach(Pollutable pollutable in pollutables){
    		pollutable.IncreasePollution(pollution);
    	}
        
    }
}
