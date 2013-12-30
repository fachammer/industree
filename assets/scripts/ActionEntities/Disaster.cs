using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Disaster :MonoBehaviour {

    public float movespeed = 5;
    public Planet planet;

    public virtual void Start(){
		audio.Play();
    }

    public virtual void Update(){
        // destroy when the disaster goes out of the screen
        if (Mathf.Abs (transform.position.x) >= 24)
            Destroy(gameObject);
    }
}
