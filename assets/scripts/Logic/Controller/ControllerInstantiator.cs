using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using assets.scripts.View;

namespace assets.scripts.Controller
{
    public class ControllerInstantiator : MonoBehaviour
    {

        void Awake()
        {
            SelectActionController.GetInstance();
        }
    }
}
