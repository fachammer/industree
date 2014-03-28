using System;
using UnityEngine;

namespace Industree.View
{
    public class UnityViewSkin : MonoBehaviour, IViewSkin
    {
        public GUISkin guiSkin;

        public GUIStyle Label
        {
            get { return guiSkin.label; }
        }
    }
}
