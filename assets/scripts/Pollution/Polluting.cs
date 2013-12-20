using UnityEngine;
using System.Collections;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;
    public Pollutable pollutable;

	void Start () {
        Timer.Instantiate(timeBetweenPollution, OnPollutionTimerTick);
	}

    void OnPollutionTimerTick()
    {
        pollutable.IncreasePollution(pollution);
    }
}
