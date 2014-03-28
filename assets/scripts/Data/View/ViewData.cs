using Industree.View;
using System;

namespace Industree.Data.View
{
    public interface IViewData
    {
        int Depth { get; }
        IViewSkin ViewSkin { get; }
    }

    [Serializable]
    public class ViewData : DataObject, IViewData
    {
        public int depth;
        public UnityViewSkin viewSkin;

        public int Depth { get { return depth; } }
        public IViewSkin ViewSkin { get { return viewSkin; } }
    }
}
