using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Thunderstorm :ActionEntity {  
    private bool hitted = false;

    public GameObject lightning;
    public GameObject lightningSpark;
	
	public AudioClip soundLightning;

    public float moveSpeed;

    private Planet planet;
    private Damaging damaging;

    private void Awake(){
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
        damaging = GetComponent<Damaging>();
    }

    private void Start(){
        GetComponent<SphericalMover>().moveSpeed *= Mathf.Sign(ActionDirection);
        transform.LookAt(planet.transform.position, Vector3.forward);
    }

    private void Update(){
        RaycastHit hit;
        if (!hitted && Physics.Linecast(transform.position, planet.transform.position,out hit, 1 << 10))
        {
			if(!hit.collider.transform.parent.parent.parent.GetComponent<Building>().Damagable.Destroyed)
			{
                damaging.CauseDamage(hit.collider.transform.parent.parent.parent.GetComponent<Damagable>());
	            SendLightning(hit.point);
	            hitted = true;
			}
        }
		
		Debug.DrawLine(transform.position,planet.transform.position,Color.green);		
    }

    private void SendLightning(Vector3 pos){
        GameObject lightningTmp =(GameObject)Instantiate(lightning, transform.position, transform.rotation);
		lightningTmp.transform.localScale = new Vector3(1,1,-1);
		Destroy(lightningTmp,0.2f);
        Instantiate(lightningSpark, pos, Quaternion.identity);
		
		audio.PlayOneShot(soundLightning);
    }
}
