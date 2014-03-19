using System;
using System.Collections.Generic;
using UnityEngine;
using Industree.Rendering;
using Industree.Facade.Internal;
using Industree.Facade;

namespace Industree.View
{
    public class CreditsView : View<CreditsViewData>
    {
        private IPlayer player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        protected override void Draw()
        {
            ResolutionIndependentRenderer.Label(data.creditRectangle, player.Credits.ToString(), data.guiSkin.label);
            ResolutionIndependentRenderer.DrawTexture(data.creditIconRectangle, data.creditIcon);
        }
    }
}
