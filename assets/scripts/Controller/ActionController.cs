using System;
using System.Collections.Generic;
using assets.scripts.View;
using UnityEngine;

namespace assets.scripts.Controller
{
    public class ActionController
    {
        private static ActionController instance;

        private GameController gameController;

        public ActionController(ActionView actionView, GameController gameController){
            actionView.ActionInput += OnActionInput;
            this.gameController = gameController;
        }

        private void OnActionInput(Player player, Action action, float actionDirection)
        {
            if (!gameController.GameEnded && !gameController.GamePaused)
            {
                bool canActionSucceed =
                    !action.IsCoolingDown &&
                    player.Credits >= action.cost &&
                    action.IsInvokable(player, actionDirection);

                if (canActionSucceed)
                {
                    action.Invoke(player, actionDirection);
                    player.SucceedAction(action);
                }
                else
                {
                    player.FailAction(action);
                }
            }
        }

        public static ActionController GetInstance()
        {
            if (instance == null)
            {
                instance = new ActionController(ActionView.Get(), GameController.Get());
            }

            return instance;
        }
    }
}
