using System;
using System.Collections.Generic;
using UnityEngine;
using Industree.Rendering;

namespace Industree.View
{
    public class ActionView : View<ActionViewData>
    {
        protected override void Draw()
        {
            ResolutionIndependentRenderer.DrawTexture(data.bounds, data.actionIcon);
        }
    }
}
