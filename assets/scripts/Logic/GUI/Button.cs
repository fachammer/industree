using System;
using UnityEngine;

namespace Industree.Controls
{
    public class Button : MonoBehaviour
    {
        public event System.Action ButtonDown = () => { };
    }
}
