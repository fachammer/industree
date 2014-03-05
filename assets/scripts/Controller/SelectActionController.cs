using assets.scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace assets.scripts.Controller
{
    public class SelectActionController
    {
        public SelectActionController(SelectActionView selectActionView)
        {
            selectActionView.ActionSelectInput += OnActionSelectInput;
        }

        private void OnActionSelectInput(Player player, float selectDirection)
        {
            if (selectDirection > 0)
            {
                player.SelectNextAction();
            }
            else if(selectDirection < 0)
            {
                player.SelectPreviousAction();
            }
        }

    }
}
