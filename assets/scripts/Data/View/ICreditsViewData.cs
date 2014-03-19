using Industree.Graphics;
using UnityEngine;

namespace Industree.Data.View
{
    public interface ICreditsViewData
    {
        Rect TextBounds { get; }
        Rect IconBounds { get; }
        ITexture Icon { get; }
    }
}
