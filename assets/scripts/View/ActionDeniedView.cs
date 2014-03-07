using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class ActionDeniedView : MonoBehaviour
    {
        public Texture deniedActionIconOverlay;
        public float deniedActionIconOverlayTime;

        private Player[] players;
        private Dictionary<Player, Dictionary<Action, Timer>> actionDeniedOverlayTimerDictionary;
        private ActionButtonInterface actionButtonInterface;

        private const int GUI_DEPTH = 0;

        private void Awake()
        {
            actionDeniedOverlayTimerDictionary = new Dictionary<Player, Dictionary<Action, Timer>>();
            players = Player.GetAll();
            actionButtonInterface = ActionButtonInterface.Get();

            foreach (Player player in players)
            {
                player.PlayerActionFailure += OnPlayerActionFailure;

                actionDeniedOverlayTimerDictionary[player] = new Dictionary<Action, Timer>();

                foreach (Action action in player.actions)
                {
                    actionDeniedOverlayTimerDictionary[player][action] = null;
                }
            }
        }

        private void OnPlayerActionFailure(Player player, Action action)
        {
            if (actionDeniedOverlayTimerDictionary[player][action] != null)
            {
                actionDeniedOverlayTimerDictionary[player][action].Stop();
            }

            actionDeniedOverlayTimerDictionary[player][action] = Timer.Start(deniedActionIconOverlayTime,
                (timer) => {
                    timer.Stop();
                    actionDeniedOverlayTimerDictionary[player][action] = null;
                });
        }

        private void OnGUI()
        {
            GUI.depth = GUI_DEPTH;
            foreach (Player player in players)
            {
                foreach (Action action in player.actions)
                {
                    if (actionDeniedOverlayTimerDictionary[player][action] != null)
                    {
                        ResolutionIndependentRenderer.DrawTexture(actionButtonInterface.GetButtonRectangleFromPlayerAndAction(player, action), deniedActionIconOverlay);
                    }
                }
            }
        }
    }
}