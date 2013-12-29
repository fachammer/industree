using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DisasterSpawnpoint: MonoBehaviour {

    public Planet planet;

    public void Start() {
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();

        //switch rotation
        this.transform.LookAt(planet.transform.position, Vector3.forward);
    }

    public void InsatantiateDisaster(Action disaster){
        Disaster d = (Disaster)Instantiate(disaster, transform.position, transform.rotation);
		
        d.movespeed = (this.transform.position.x > 0) ? -d.movespeed : d.movespeed;
    }
}

