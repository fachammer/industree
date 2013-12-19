using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Disaster:Interactive
{
    public float movespeed = 5;
    public int damage = 1;
    public Planet planet;

    public virtual void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		
		audio.Play();

    }

    public override bool performAction(Player player, float castDirection)
    {
        base.performAction(player, castDirection);

        //Find all spawnpoints and spawn mine
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        foreach (GameObject item in spawnPoints)
        {
            if(item.transform.position.x > 0 && castDirection < 0 || 
                item.transform.position.x < 0 && castDirection > 0)
                item.GetComponent<DisasterSpawnpoint>().insatantiateDisaster(this);
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

