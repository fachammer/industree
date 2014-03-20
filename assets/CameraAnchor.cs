using UnityEngine;
using Industree.Facade;

public class CameraAnchor : MonoBehaviour {

    private void OnBecameVisible()
    {
        GameFactory.GetGameInstance().StartGame();
    }
}
