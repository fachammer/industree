using UnityEngine;
using System.Collections;

public class PollutionScreenFader : MonoBehaviour {
	
	public Light sun;
	public Color cleanColor;
	public Color pollutedColor;

	private Pollutable pollutable;

	private void Awake(){
		pollutable = (Pollutable) GameObject.FindObjectOfType(typeof(Pollutable));
	}

	private void Update () {
		sun.color = Color.Lerp(cleanColor, pollutedColor, pollutable.currentPollution / pollutable.maxPollution);
	}
}
