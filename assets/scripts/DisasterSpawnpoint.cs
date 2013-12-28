using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DisasterSpawnpoint: Action {
    public Player player;
    public Planet planet;

    public void Start() {
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();

        //switch rotation
        this.transform.LookAt(planet.transform.position, Vector3.forward);
        //this.transform.Rotate(new Vector3(0, 90, 0), Space.World);
    }

    public override void Perform(Player player, float actionDirection) {
        //switch position
        this.transform.position = new Vector3(-this.transform.position.x, this.transform.position.y, this.transform.position.z);

        //switch direction
        this.transform.localScale = new Vector3(-this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
		
		this.transform.LookAt(planet.transform.position, Vector3.forward);
    }

    public void InsatantiateDisaster(Action disaster){
        Disaster d = (Disaster)Instantiate(disaster, transform.position, transform.rotation);
		
        d.movespeed = (this.transform.position.x > 0) ? -d.movespeed : d.movespeed;
    }
}

