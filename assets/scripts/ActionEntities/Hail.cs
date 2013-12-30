using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Hail :ActionEntity {

    public Building currentTarget;

    [Range(0, 1)]
    public float probability = 1;

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
        if (Physics.Linecast(transform.position, planet.transform.position, out hit, 1 << 10))
        {
            if (!hit.collider.transform.parent.parent.parent.GetComponent<Building>().Damagable.Destroyed && currentTarget != hit.collider.transform.parent.parent.parent.GetComponent<Building>())
            {
                currentTarget=hit.collider.transform.parent.parent.parent.GetComponent<Building>();

                if (probability >= UnityEngine.Random.Range(0f, 1f))
                {
                    damaging.CauseDamage(currentTarget.Damagable);
            	}
              
			}
        }

        Debug.DrawLine(transform.position, planet.transform.position, Color.green);
    }
}