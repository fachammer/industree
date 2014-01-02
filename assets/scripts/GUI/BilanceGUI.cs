using UnityEngine;
using System.Collections;

public class BilanceGUI : MonoBehaviour {

	public Texture2D bilanceDecoration;
	public Color bilanceAirColor;
	public Color bilancePollutionColor;
	public Vector2 bilanceSize;
	public float bilanceTopOffset;

	private Pollutable pollutable;

	private Rect bilanceAirRectangle;
    private Rect bilancePollutionRectangle;
	private Texture2D bilanceAirTexture;
    private Texture2D bilancePollutionTexture;

	private void Awake(){
		pollutable = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Pollutable>();

		bilanceAirRectangle = new Rect(
			(Screen.width - bilanceSize.x) / 2, 
			bilanceTopOffset, 
			bilanceSize.x, 
			bilanceSize.y);
        bilancePollutionRectangle = new Rect(
        	(Screen.width - bilanceSize.x) / 2, 
        	bilanceTopOffset, 
        	bilanceSize.x, 
        	bilanceSize.y);

        bilanceAirTexture = Utilities.MakeTexture2DWithColor(bilanceAirColor);
        bilancePollutionTexture = Utilities.MakeTexture2DWithColor(bilancePollutionColor);
	}

	private void OnGUI(){
		float pollution =  Mathf.Clamp(pollutable.currentPollution, 0, pollutable.maxPollution);
        bilancePollutionRectangle.width = bilanceAirRectangle.width * pollution / pollutable.maxPollution;

		GUI.DrawTexture(bilanceAirRectangle, bilanceAirTexture, ScaleMode.StretchToFill);
        GUI.DrawTexture(bilancePollutionRectangle, bilancePollutionTexture, ScaleMode.StretchToFill);

        // +3 on x to compensate for the inprecision in the asset
		GUI.DrawTexture(new Rect((Screen.width - bilanceDecoration.width) / 2 + 3, 0, bilanceDecoration.width, bilanceDecoration.height), bilanceDecoration);  
	}
}
