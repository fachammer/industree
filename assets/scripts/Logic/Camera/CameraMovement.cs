using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraMovement:MonoBehaviour
{
    public Renderer cameraAnchor;

    public float speed;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>().GameStart += OnGameStart;
    }   

    private void OnGameStart()
    {
        Destroy(this);
    }

    private void Update()
    {
        GetComponent<Camera>().orthographicSize += speed * Time.deltaTime;
    }
}
