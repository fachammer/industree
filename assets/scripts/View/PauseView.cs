using UnityEngine;
using System.Collections;
using Industree.Rendering;

namespace Industree.View
{
    public class PauseView : View<PauseViewData>
    {
        private GameController gameController;

        private void Awake()
        {
            gameController = GameController.Get();
        }

        protected override void Draw()
        {
            if (gameController.GamePaused && !gameController.GameEnded)
            {
                ResolutionIndependentRenderer.DrawTexture(data.pauseDialogRectangle, data.pauseDialog);
            }
        }
    }
}
