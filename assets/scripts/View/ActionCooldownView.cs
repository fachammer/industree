using UnityEngine;
using System.Collections;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class ActionCooldownView : MonoBehaviour
    {
        public Texture cooldownActionIconOverlay;

        private ActionButtonInterface actionButtonInterface;
        private Player[] players;

        private const int GUI_DEPTH = 1;

        private void Awake()
        {
            actionButtonInterface = ActionButtonInterface.Get();
            players = Player.GetAll();
        }

        private void OnGUI()
        {
            GUI.depth = GUI_DEPTH;

            foreach (Player player in players)
            {
                foreach (Action action in player.actions)
                {
                    if (action.IsCoolingDown)
                    {
                        Rect drawRectangle = CalculateCooldownOverlayRectangle(player, action);
                        ResolutionIndependentRenderer.DrawTexture(drawRectangle, cooldownActionIconOverlay);
                    }
                   
                }
            }
        }

        private Rect CalculateCooldownOverlayRectangle(Player player, Action action)
        {
            Rect actionIconRectangle = actionButtonInterface.GetButtonRectangleFromPlayerAndAction(player, action);
            float overlayWidth = Mathf.Clamp(
                action.GetRemainingCooldown() * actionIconRectangle.width / action.cooldown,
                0,
                actionIconRectangle.width);

            if (player.side == Player.Side.left)
            {
                return new Rect(
                        actionIconRectangle.x,
                        actionIconRectangle.y,
                        overlayWidth,
                        actionIconRectangle.height);
            }
            else
            {
                return new Rect(
                        actionIconRectangle.x + (actionIconRectangle.width - overlayWidth),
                        actionIconRectangle.y,
                        overlayWidth,
                        actionIconRectangle.height);
            }
        }
    }
}
