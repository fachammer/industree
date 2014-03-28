using UnityEngine;
using System;
using Industree.Facade;
using Industree.Time.Internal;
using Industree.Time;

public class Earthquake :ActionEntity {

    public float hurtDeltaTime;
    public float shakeIntensity;
    
    private IGame game;
    private Damaging damaging;
    private Vector3 initialCameraPosition;
    private Building[] buildings;
    private ITimer damageTimer;

    private void Awake(){
        game = GameFactory.GetGameInstance();
        damaging = GetComponent<Damaging>();
        
        game.GameEnd += Disable;
        game.GamePause += Disable;
        game.GameResume += Disable;
    }

    private void Start(){
        initialCameraPosition = Camera.main.transform.position;
        buildings = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Building)), obj => (Building) obj);
        damageTimer = Timing.GetTimerFactory().GetTimer(hurtDeltaTime);
        damageTimer.Tick += OnDamageTimerTick;
    }

    private void Disable()
    {
        enabled = false;
    }

    private void OnDamageTimerTick(ITimer timer){
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

        game.GameEnd -= Disable;
        game.GamePause -= Disable;
        game.GameResume -= Disable;
            
        damageTimer.Stop();
	}

}

