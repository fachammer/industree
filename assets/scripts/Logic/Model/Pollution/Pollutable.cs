using UnityEngine;
using System.Collections;

public class Pollutable : MonoBehaviour {

    public int currentPollution;
    public int maxPollution;

    public delegate void PollutionHandler(Pollutable pollutable);

    public event PollutionHandler FullPollution = delegate(Pollutable pollutable) {};
    public event PollutionHandler NoPollution = delegate(Pollutable pollutable) {};

    public void IncreasePollution(int pollution){
    	currentPollution += pollution;

    	if(currentPollution >= maxPollution){
    		FullPollution(this);
    	}
    	else if(currentPollution <= 0){
    		NoPollution(this);
    	}
    }

    public static Pollutable Get()
    {
        return GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Pollutable>();
    }
}
