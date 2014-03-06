using UnityEngine;
using System.Collections;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class WinLoseView : MonoBehaviour
    {

        public Rect dialogRectangle;
        public Texture2D winDialog;
        public Texture2D loseDialog;

        private GameController gameController;

        private void Awake()
        {
            gameController = GameController.Get();
        }

        private void OnGUI()
        {
            if (gameController.GameEnded)
            {
                if (gameController.GameWon)
                {
                    ResolutionIndependentRenderer.DrawTexture(dialogRectangle, winDialog);
                }
                else
                {
                    ResolutionIndependentRenderer.DrawTexture(dialogRectangle, loseDialog);
                }
            }
        }
    }
}
