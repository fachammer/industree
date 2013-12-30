using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tornado :ActionEntity
{
    public float duration;

    [Range(0,1)]
    public float probability;

    public float moveRange;
    public float damageInterval;

    private Damaging damaging;
    private Planet planet;

    private void Awake(){
        damaging = GetComponent<Damaging>();
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
        Timer.AddTimer(gameObject, damageInterval, OnDamageTimerTick);
    }

    private void Start(){
        
        transform.LookAt(planet.transform.position);
        transform.Rotate(new Vector3(-90, 0, 0));

        foreach (AnimationState state in animation) {
            state.speed = 2.5f;
        }
        
        Destroy(gameObject, duration);
    }

    private void OnDamageTimerTick(Timer timer){
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f, Layers.Building);
        foreach (Collider c in colliders){
            if (UnityEngine.Random.Range(0f, 1f) <= probability){
                damaging.CauseDamage(c.transform.parent.parent.parent.GetComponent<Damagable>());
            }
        }   
    }

    private void Update(){
        float moveSpeed = GetComponent<SphericalMover>().moveSpeed;
        GetComponent<SphericalMover>().moveSpeed = Mathf.Lerp(-moveRange, moveRange, Mathf.Sin(Time.time / moveSpeed));
    }
}

