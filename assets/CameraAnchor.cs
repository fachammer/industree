using UnityEngine;
using System.Collections;

public class CameraAnchor : MonoBehaviour {

    private void OnBecameVisible()
    {
        GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().StartGame();	
    }
}
