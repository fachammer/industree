using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Earthquake :ActionEntity
{
    public float duration = 2;
    public float hurtDeltaTime = 0.3f;

    public Vector3 originPosition;
    public Quaternion originRotation;

    public float shake_intensity;

    public List<Building> buildingList;

    private float lastTime;

    private GameController gameController;

    private Damaging damaging;

    private void Start(){
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        
        gameController.GameEnd += OnGameEnd;
        gameController.GamePause += OnGamePause;
        gameController.GameResume += OnGameResume;

        damaging = GetComponent<Damaging>();

        Destroy(gameObject, duration);

        originPosition = Camera.main.transform.position;
        originRotation = Camera.main.transform.rotation;
        shake_intensity = .5f;

        lastTime = Time.time;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag(Tags.building))
        {
            buildingList.Add(go.GetComponent<Building>());
        }
		
		transform.position = Camera.main.transform.position;
    }

    private void OnGameEnd(bool win){
        enabled = false;
    }

    private void OnGamePause(){
        enabled = false;
    }

    private void OnGameResume(){
        enabled = true;
    }

    private void Update(){		
		//shake
		
		
        Camera.main.transform.position = originPosition + new Vector3(UnityEngine.Random.insideUnitCircle.x,UnityEngine.Random.insideUnitCircle.y,0)*shake_intensity;
		
		shake_intensity-=Time.deltaTime*shake_intensity;

        //Hurt
        if (Time.time >= lastTime + hurtDeltaTime){
			Building b=buildingList[UnityEngine.Random.Range(0, buildingList.Count - 1)];
			
			if(b){
                damaging.damage = UnityEngine.Random.Range(0, damaging.damage);
                damaging.CauseDamage(b.Damagable);
            }
        }
    }
	
	private void OnDestroy(){
		Camera.main.transform.position = originPosition;
		Camera.main.transform.rotation = originRotation;

        gameController.GameEnd -= OnGameEnd;
        gameController.GamePause -= OnGamePause;
        gameController.GameResume -= OnGameResume;
	}

}

