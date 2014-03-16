using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class CreditsView : View<CreditsViewData>
    {
        private CreditsManager creditsManager;

        private void Awake()
        {
            creditsManager = GetComponent<CreditsManager>();
        }

        protected override void Draw()
        {
            ResolutionIndependentRenderer.Label(data.creditRectangle, creditsManager.Credits.ToString(), data.guiSkin.label);
            ResolutionIndependentRenderer.DrawTexture(data.creditIconRectangle, data.creditIcon);
        }
    }
}
