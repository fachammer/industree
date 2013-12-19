using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollutionSystem : MonoBehaviour {

    public List<Polluting> pollutings;
    public Pollutable pollutable;

	void Start () {
        foreach (Polluting p in pollutings)
        {
            p.Pollute += OnPollute;
        }
	}

    void OnPollute(Polluting polluting)
    {
        pollutable.currentPollution += polluting.pollution;
    }
}
