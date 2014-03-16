using System;
using System.Collections.Generic;
using assets.scripts.View;
using UnityEngine;

namespace assets.scripts.Controller
{
    public class ActionInvoker : MonoBehaviour
    {
        private PlayerInput playerInputInterface;
        private GameController gameController;
        private CreditsManager creditsManager;

        public event System.Action<Player, Action, float> ActionSuccess = (player, action, actionDirection) => { };
        public event System.Action<Player, Action, float> ActionFailure = (player, action, actionDirection) => { };

        private void Awake()
        {
            gameController = GameController.Get();
            playerInputInterface = GetComponent<PlayerInput>();
            creditsManager = GetComponent<CreditsManager>();
            playerInputInterface.PlayerActionInput += OnActionInput;
        }

        private void OnActionInput(Player player, float actionDirection)
        {
            if (!gameController.GameEnded && !gameController.GamePaused)
            {
                Action action = player.SelectedAction;
                bool canActionSucceed =
                    !action.IsCoolingDown &&
                    creditsManager.Credits >= action.cost &&
                    action.IsInvokable(player, actionDirection);

                if (canActionSucceed)
                {
                    action.Invoke(player, actionDirection);
                    ActionSuccess(player, action, actionDirection);
                }
                else
                {
                    ActionFailure(player, action, actionDirection);
                }
            }
        }
    }
}
