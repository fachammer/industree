using System;
using System.Collections.Generic;
using assets.scripts.View;
using UnityEngine;

namespace assets.scripts.Controller
{
    public class ActionController
    {
        private static ActionController instance;

        public ActionController(ActionView actionView){
            actionView.ActionInput += OnActionInput;
        }

        private void OnActionInput(Player player, Action action, float actionDirection)
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

        public static ActionController GetInstance()
        {
            if (instance == null)
            {
                instance = new ActionController(ActionView.Get());
            }

            return instance;
        }
    }
}
