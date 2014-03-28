using Industree.Logic.StateManager;
using Industree.Time;
using Industree.Time.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Building : MonoBehaviour{

    public float[] minLevelUpTimes;
    public float[] maxLevelUpTimes;

    public GameObject[] buildingLevelModels;
    public int[] pollutionLevels;
    public int[] hitpointLevels;

    public float destroyDelay;

    private float[] levelUpTimes;
    private float dyingSpeed = 0;

    private Polluting polluting;
    private Damagable damagable;
    private LevelManager levelManager;

    public LevelManager LevelManager {
        get { return levelManager; }
    }

    public Damagable Damagable {
        get { return damagable; }
    }

    private void Awake(){
        polluting = GetComponent<Polluting>();
        levelManager = new LevelManager(1);
        damagable = GetComponent<Damagable>();
        levelManager.LevelUp += OnLevelUp;
        damagable.BeforeDestroy += OnBuildingDestroy;
    }
	
	private void Start(){
        damagable.Hitpoints = hitpointLevels[0];
        polluting.pollution = pollutionLevels[0];

        levelUpTimes = new float[minLevelUpTimes.Length];
		for(int i = 0; i < levelUpTimes.Length; i++){
	        levelUpTimes[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}

        Timing.GetTimerFactory().GetTimer(levelUpTimes[0]).Tick += OnLevelUpTimerTick;

        GameObject newGameObject = (GameObject)Instantiate(buildingLevelModels[0], transform.position, transform.rotation);
        newGameObject.transform.parent = transform;
	}

    private void OnLevelUpTimerTick(ITimer timer)
    {
        levelManager.RaiseLevel();

        if (levelManager.Level < 4)
        {
            timer.Stop();
            Timing.GetTimerFactory().GetTimer(levelUpTimes[levelManager.Level - 1]).Tick += OnLevelUpTimerTick;
        }
        else
        {
            timer.Stop();
        }
    }

    private void Update(){

        // Animate if destroyed
        if(damagable.Destroyed){
            dyingSpeed += 0.1f;
            transform.position -= transform.up * Time.deltaTime * dyingSpeed;
        }
    }

    private void OnLevelUp(int oldLevel, int newLevel)
    {
        damagable.Hitpoints = hitpointLevels[newLevel - 1];
        polluting.pollution = pollutionLevels[newLevel - 1];

        // Destroy children and add new model as current model
        foreach(Transform t in transform){
            Destroy(t.gameObject);
        }

        GameObject newGameObject = (GameObject)Instantiate(buildingLevelModels[newLevel - 1], transform.position, transform.rotation);
        newGameObject.transform.parent = transform;
    }

    private void OnBuildingDestroy(Damagable damagable){
    	levelManager.LevelUp -= OnLevelUp;
    	damagable.BeforeDestroy -= OnBuildingDestroy;
        Destroy(gameObject, destroyDelay);
    }
}

