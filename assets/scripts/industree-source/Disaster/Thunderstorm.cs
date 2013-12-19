using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Thunderstorm:Disaster
{  
    private bool hitted = false;

    public GameObject lightning;
    public GameObject lightningSpark;
	
	public AudioClip soundLightning;

    public override void Update()
    {
        base.Update();

        RaycastHit hit;
        if (!hitted && Physics.Linecast(transform.position, planet.transform.position,out hit, 1 << 10))
        {
			if(hit.collider.transform.parent.parent.GetComponent<Building>().alive)
			{
	            hit.collider.transform.parent.parent.GetComponent<Building>().takeDamage(damage);
	            sendLightning(hit.point);
	            hitted = true;
			}
        }
		
		Debug.DrawLine(transform.position,planet.transform.position,Color.green);		
		
        move();       
    }

    public void move()
    {
        transform.RotateAround(planet.transform.position, Vector3.back, movespeed * Time.deltaTime);
    }

    public void sendLightning(Vector3 pos)
    {
        GameObject lightningTmp =(GameObject)Instantiate(lightning, transform.position, transform.rotation);
		lightningTmp.transform.localScale = new Vector3(1,1,-1);
		Destroy(lightningTmp,0.2f);
        Instantiate(lightningSpark, pos, Quaternion.identity);
		
		audio.PlayOneShot(soundLightning);
    }
	
	

}

