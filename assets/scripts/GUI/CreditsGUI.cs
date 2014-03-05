using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsGUI : MonoBehaviour {

	public Texture2D creditsIcon;
	public float creditsTopOffset;

	private Dictionary<Player, Rect> creditLabelRectsDictionary;
	private Dictionary<Player, Rect> creditIconRectsDictionary;

	private const float CREDITS_ICON_TOP_OFFSET = 30;

	private void Awake(){
		Player[] players = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().players;

		creditLabelRectsDictionary = new Dictionary<Player, Rect>();
		creditIconRectsDictionary = new Dictionary<Player, Rect>();

		foreach(var player in players){
			creditLabelRectsDictionary[player] = CalculateCreditsLabelRectangle(player);
			creditIconRectsDictionary[player] = CalculateCreditsIconRectangle(player);
		}
	}

	private void OnGUI(){
		GUI.skin.font = GameObject.FindGameObjectWithTag(Tags.style).GetComponent<Style>().font;

		foreach(var creditLabelRectEntry in creditLabelRectsDictionary){
			Player player = creditLabelRectEntry.Key;
			Rect creditLabelRect = creditLabelRectEntry.Value;
			GUI.Label(creditLabelRect, player.Credits.ToString());
		}

		foreach(var creditIconRect in creditIconRectsDictionary.Values){
			GUI.DrawTexture(creditIconRect, creditsIcon);
		}
	}

	private Rect CalculateCreditsLabelRectangle(Player player){
		float iconSize = player.actions[0].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);
	    return new Rect(iconXOffset, creditsTopOffset, iconSize, iconSize);
	}

	private Rect CalculateCreditsIconRectangle(Player player){
		float iconSize = player.actions[0].icon.width;
		float iconXOffset = (player.side == Player.Side.left) ? 0 : (Screen.width - iconSize);

		if (player.side == Player.Side.left) {
        	return new Rect(
        		iconXOffset + iconSize, 
        		creditsIcon.height + CREDITS_ICON_TOP_OFFSET, 
        		creditsIcon.width, 
        		creditsIcon.height);
        }
        else {
        	return new Rect(
        		iconXOffset - creditsIcon.width, 
        		creditsIcon.height + CREDITS_ICON_TOP_OFFSET, 
        		creditsIcon.width, 
        		creditsIcon.height);
        }
	}
}
