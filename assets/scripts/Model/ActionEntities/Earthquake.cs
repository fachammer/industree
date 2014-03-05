using UnityEngine;
using System;

public class Earthquake :ActionEntity {

    public float hurtDeltaTime;
    public float shakeIntensity;
    
    private GameController gameController;
    private Damaging damaging;
    private Vector3 initialCameraPosition;
    private Building[] buildings;

    private void Awake(){
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        damaging = GetComponent<Damaging>();
        
        gameController.GameEnd += OnGameEnd;
        gameController.GamePause += OnGamePause;
        gameController.GameResume += OnGameResume;
    }

    private void Start(){
        initialCameraPosition = Camera.main.transform.position;
        buildings = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Building)), obj => (Building) obj);
        Timer.AddTimerToGameObject(gameObject, hurtDeltaTime, OnDamageTimerTick);
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

    private void OnDamageTimerTick(Timer timer){
        int randomBuildingIndex = UnityEngine.Random.Range(0, buildings.Length);
        Building building = buildings[randomBuildingIndex];
            
        if(building){
            damaging.damage = UnityEngine.Random.Range(0, damaging.damage);
            damaging.CauseDamage(building.Damagable);
        }
    }

    private void Update(){
        ShakeCamera();
    }

    private void ShakeCamera(){
        Camera.main.transform.position = initialCameraPosition + new Vector3(UnityEngine.Random.insideUnitCircle.x, UnityEngine.Random.insideUnitCircle.y, 0) * shakeIntensity;
        shakeIntensity -= Time.deltaTime * shakeIntensity;
    }
	
	private void OnDestroy(){
		Camera.main.transform.position = initialCameraPosition;

        gameController.GameEnd -= OnGameEnd;
        gameController.GamePause -= OnGamePause;
        gameController.GameResume -= OnGameResume;
	}

}

