using UnityEngine;
using System.Collections;

public class Polluting : MonoBehaviour {

    public int pollution;
    public float timeBetweenPollution;

    public delegate void PolluteHandler(Polluting polluting);
    public event PolluteHandler Pollute;

	// Use this for initialization
	void Start () {
        Timer.Instantiate(timeBetweenPollution, OnPollutionTimerTick);
	}

    void OnPollutionTimerTick()
    {
        Pollute(this);
    }
}
