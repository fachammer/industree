using assets.scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Controller
{
    public class SelectActionController
    {
        private static SelectActionController instance;

        public SelectActionController(SelectActionView selectActionView)
        {
            selectActionView.ActionSelectInput += OnActionSelectInput;
        }

        private void OnActionSelectInput(Player player, float selectDirection)
        {
            if (selectDirection < 0)
            {
                player.SelectNextAction();
            }
            else if(selectDirection > 0)
            {
                player.SelectPreviousAction();
            }
        }

        public static SelectActionController GetInstance()
        {
            if (instance == null)
            {
                instance = new SelectActionController(SelectActionView.Get());
            }

            return instance;
        }

    }
}
