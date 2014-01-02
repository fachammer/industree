using UnityEngine;
using System.Collections;

public class PollutionScreenFader : MonoBehaviour {
	
	public Light sun;
	public Color cleanColor;
	public Color pollutedColor;

	private Pollutable pollutable;
	private Planet planet;

	private void Awake(){
		pollutable = (Pollutable) GameObject.FindObjectOfType(typeof(Pollutable));
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
	}

	private void Update () {
		sun.color = Color.Lerp(cleanColor, pollutedColor, pollutable.currentPollution / planet.air);
	}
}
