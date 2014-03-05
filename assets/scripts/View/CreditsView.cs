using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class CreditsView : MonoBehaviour
    {
        public GUISkin guiSkin;
        public Texture creditIcon;
        public Rect[] creditIconRectangles;
        public Rect[] creditRectangles;

        private Player[] players;

        private void Awake()
        {
            players = Player.GetAll();
        }

        private void OnGUI()
        {
            foreach(Player player in players){
                DrawCredits(player);
            }
        }

        private void DrawCredits(Player player)
        {
            ResolutionIndependentRenderer.Label(creditRectangles[player.index], player.Credits.ToString(), guiSkin.label);
            ResolutionIndependentRenderer.DrawTexture(creditIconRectangles[player.index], creditIcon);
        }
    }
}
