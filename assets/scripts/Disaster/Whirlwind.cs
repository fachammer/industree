using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Whirlwind:Disaster
{
    public int xRange = 10;
    public float duration = 3;

    [Range(0,1)]
    public float probability = 1;

    public float moveRange = 3;

    private float speed=0;

    private float lastTime=0;

    public override void Start()
    {
        base.Start();
        
        //Set the position
		Vector3 pos = new Vector3(UnityEngine.Random.Range(-xRange,xRange),60,planet.transform.position.z);
        RaycastHit hit;
        
		if(Physics.Raycast(pos,Vector3.down,out hit,100,~(1<<10)))
        {
			
            transform.position = hit.point;
            this.transform.LookAt(planet.transform.position);
			this.transform.Rotate(new Vector3(-90,0,0));
        }
		
		Debug.DrawRay(pos,Vector3.down*60);

        Destroy(this.gameObject, duration);
		
		foreach (AnimationState state in animation) 
		{
			state.speed = 2.5f;
		}
    }

    public override void Update()
    {
        base.Update();

        //Move
        transform.RotateAround(planet.transform.position, Vector3.back, speed * Time.deltaTime);
        speed = Mathf.Lerp(-moveRange, moveRange, Mathf.Sin(Time.time / movespeed));

        //Hurt
        if (Time.time > lastTime + 0.3f)
        {
            foreach (Collider c in Physics.OverlapSphere(this.transform.position, 0.3f, 1 << 10))
            {
                if (UnityEngine.Random.Range(0f, 1f) <= probability)
                {
                    c.transform.parent.parent.GetComponent<Building>().Damagable.TakeDamage(damage);
                }
            }
            lastTime = Time.time;
        }
    }
}

