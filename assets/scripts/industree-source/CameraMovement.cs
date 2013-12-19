using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraMovement:MonoBehaviour
{
    public GameObject edgeLeft;
    public GameObject edgeRight;

    public float speed=5;

    public void Update()
    {
        this.GetComponent<Camera>().orthographicSize += speed * Time.deltaTime;

        if (edgeLeft.GetComponentInChildren<Renderer>().isVisible && edgeRight.GetComponentInChildren<Renderer>().isVisible){
			GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>().started = true;	
            Destroy(this);
		}
    }
}
