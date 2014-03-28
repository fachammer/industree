using System;
using UnityEngine;

namespace Industree.Data.View
{
    public interface ISelectedActionViewData
    {
        Texture IconOverlay { get; }
    }

    [Serializable]
    public class SelectedActionViewData : ViewData, ISelectedActionViewData
    {
        public Texture selectedActionIconOverlay;

        public Texture IconOverlay { get { return selectedActionIconOverlay; } }
    }
}
