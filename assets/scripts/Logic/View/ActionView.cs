using System;
using System.Collections.Generic;
using UnityEngine;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class ActionView : View<ActionViewData>
    {
        protected override void Draw()
        {
            ResolutionIndependentRenderer.DrawTexture(data.bounds, data.actionIcon);
        }
    }
}
