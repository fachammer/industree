using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Hail:Disaster
{
    public Building currentTarget;

    [Range(0, 1)]
    public float probability = 1;

    public override void Update()
    {
        base.Update();

        RaycastHit hit;
        if (Physics.Linecast(transform.position, planet.transform.position, out hit, 1 << 10))
        {
            if (hit.collider.transform.parent.parent.GetComponent<Building>().alive && currentTarget != hit.collider.transform.parent.parent.GetComponent<Building>())
            {
                currentTarget=hit.collider.transform.parent.parent.GetComponent<Building>();

                if (probability >= UnityEngine.Random.Range(0f, 1f))
                {
                	currentTarget.takeDamage(damage);
            	}
              
			}
        }

        Debug.DrawLine(transform.position, planet.transform.position, Color.green);

        move();
    }

    public void move()
    {
        transform.RotateAround(planet.transform.position, Vector3.back, movespeed * Time.deltaTime);
    }
}

