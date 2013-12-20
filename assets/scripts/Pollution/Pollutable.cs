using UnityEngine;
using System.Collections;

public class Pollutable : MonoBehaviour {

    public int currentPollution;

    public delegate void PollutedHandler(Pollutable pollutable, int pollution);
    public event PollutedHandler Pollute = delegate(Pollutable pollutable, int pollution){};

    public void IncreasePollution(int pollution){
    	currentPollution += pollution;
    	Pollute(this, pollution);
    }
}
