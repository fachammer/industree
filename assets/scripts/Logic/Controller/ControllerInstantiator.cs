using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Industree.View;

namespace Industree.Controller
{
    public class ControllerInstantiator : MonoBehaviour
    {

        void Awake()
        {
            SelectActionController.GetInstance();
        }
    }
}
