using System;
using UnityEngine;

namespace assets.scripts.Controls
{
    public class Button : MonoBehaviour
    {
        public event System.Action ButtonDown = () => { };
    }
}
