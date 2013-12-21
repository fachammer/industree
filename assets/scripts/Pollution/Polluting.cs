using UnityEngine;
using System;
using System.Collections;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;

    [HideInInspector]
    private Pollutable[] pollutables;

	void Start () {
		pollutables = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Pollutable)), item => (Pollutable)item);
        Timer.Instantiate(timeBetweenPollution, OnPollutionTimerTick);
	}

    void OnPollutionTimerTick()
    {
    	foreach(Pollutable pollutable in pollutables){
    		pollutable.IncreasePollution(pollution);
    	}
        
    }
}
