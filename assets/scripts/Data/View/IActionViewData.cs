using Industree.Graphics;
using UnityEngine;

namespace Industree.Data.View
{
    public interface IActionViewData
    {
        ITexture Icon { get; }
        Rect IconBounds { get; }
        Rect CostBounds { get; }
    }
}
