using Industree.Facade;
using Industree.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Industree.Controller
{
    public class SelectActionController
    {
        private static SelectActionController instance;

        private GameController gameController;

        public SelectActionController(SelectedActionView[] selectActionViews, GameController gameController)
        {
            foreach (SelectedActionView selectActionView in selectActionViews)
            {
                selectActionView.ActionSelectInput += OnActionSelectInput;
            }
            
            this.gameController = gameController;
        }

        private void OnActionSelectInput(IPlayer player, float selectDirection)
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
                instance = new SelectActionController(SelectedActionView.GetAll(), GameController.Get());
            }

            return instance;
        }

    }
}
