using UnityEngine;
using System.Collections;
using assets.scripts.Rendering;

namespace assets.scripts.View
{
    public class PauseView : MonoBehaviour
    {
        public Rect pauseDialogRectangle;
        public Texture pauseDialog;

        private GameController gameController;

        private void Awake()
        {
            gameController = GameController.Get();
        }

        private void OnGUI()
        {
            if (gameController.GamePaused && !gameController.GameEnded)
            {
                ResolutionIndependentRenderer.DrawTexture(pauseDialogRectangle, pauseDialog);
            }
        }
    }
}
