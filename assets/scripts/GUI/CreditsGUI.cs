using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour {

	public Texture2D creditsIcon;
	public float creditsTopOffset;

	private Player[] players;
	private Action[] actions;
	private Rect[] creditLabelRects;
	private Rect[] creditIconRects;

	private void Awake(){
		players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;
		actions = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<ActionInvoker>().actions;

		creditLabelRects = new Rect[players.Length];
		creditIconRects = new Rect[players.Length];

		for(int i = 0; i < players.Length; i++){
			creditLabelRects[i] = CalculateCreditsLabelRectangle(i);
			creditIconRects[i] = CalculateCreditsIconRectangle(i);
		}
	}

	private void OnGUI(){
		GUI.skin.font = GameObject.FindGameObjectWithTag(Tags.style).GetComponent<Style>().font;

		for(int i = 0; i < players.Length; i++){
	        GUI.Label(creditLabelRects[i], players[i].credits.ToString());        
	        GUI.DrawTexture(creditIconRects[i], creditsIcon);
	    }
	}

	private Rect CalculateCreditsLabelRectangle(int playerIndex){
		Player player = players[playerIndex];
		float iconSize = actions[0].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
	    return new Rect(iconXOffset, creditsTopOffset, iconSize, 30);
	}

	private Rect CalculateCreditsIconRectangle(int playerIndex){
		Player player = players[playerIndex];
		Rect creditLabelRect = creditLabelRects[playerIndex];
		if (player.side == Player.Side.left) {
        	return new Rect(
        		creditLabelRect.xMax, 
        		creditLabelRect.height + creditsIcon.height, 
        		creditsIcon.width, 
        		creditsIcon.height);
        }
        else {
        	return new Rect(
        		creditLabelRect.xMin - creditsIcon.width, 
        		creditLabelRect.height + creditsIcon.height, 
        		creditsIcon.width, 
        		creditsIcon.height);
        }
	}
}
