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

        private GameController gameController;

        public SelectActionController(SelectActionView selectActionView, GameController gameController)
        {
            selectActionView.ActionSelectInput += OnActionSelectInput;
            this.gameController = gameController;
        }

        private void OnActionSelectInput(Player player, float selectDirection)
        {
            if (!gameController.GameEnded && !gameController.GamePaused)
            {
                if (selectDirection < 0)
                {
                    player.SelectNextAction();
                }
                else if (selectDirection > 0)
                {
                    player.SelectPreviousAction();
                }
            }
        }

        public static SelectActionController GetInstance()
        {
            if (instance == null)
            {
                instance = new SelectActionController(SelectActionView.Get(), GameController.Get());
            }

            return instance;
        }

    }
}
