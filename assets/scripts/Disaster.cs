using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Disaster: Action
{
    public float movespeed = 5;
    public int damage = 1;
    public Planet planet;

    public virtual void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		
		audio.Play();

    }

    public override bool performAction(Player player, float actionDirection)
    {
        base.performAction(player, actionDirection);

        //Find all spawnpoints and spawn mine
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Tags.spawnPoint);

        foreach (GameObject item in spawnPoints)
        {
            if(item.transform.position.x > 0 && actionDirection < 0 || 
                item.transform.position.x < 0 && actionDirection > 0)
                item.GetComponent<DisasterSpawnpoint>().InsatantiateDisaster(this);
        }
		
		return true;
    }

    public virtual void Update()
    {
        //Nach einiger Zeit zerstören
        if (Mathf.Abs (transform.position.x) >= 24)
            Destroy(gameObject);
    }
}

