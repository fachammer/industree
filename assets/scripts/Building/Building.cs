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

    private float dyingSpeed = 0;

    private Polluting polluting;
    private Damagable damagable;
    private Levelable levelable;

    public Levelable Levelable {
        get { return levelable; }
    }

    public Damagable Damagable {
        get { return damagable; }
    }

    private void Awake(){
        polluting = GetComponent<Polluting>();
        levelable = GetComponent<Levelable>();
        damagable = GetComponent<Damagable>();
        levelable.LevelUp += OnLevelUp;
        damagable.BeforeDestroy += OnBuildingDestroy;
    }
	
	private void Start(){
        damagable.Hitpoints = hitpointLevels[0];
        polluting.pollution = pollutionLevels[0];
		for(int i = 0; i < minLevelUpTimes.Length; i++){
	        levelable.levelUpTimes[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}

        GameObject newGameObject = (GameObject)Instantiate(buildingLevelModels[0], transform.position, transform.rotation);
        newGameObject.transform.parent = transform;
	}

    private void Update(){

        // Animate if destroyed
        if(damagable.Destroyed){
            dyingSpeed += 0.1f;
            transform.position -= transform.up * Time.deltaTime * dyingSpeed;
        }
    }

    private void OnLevelUp(Levelable levelable)
    {
        damagable.Hitpoints = hitpointLevels[levelable.Level - 1];
        polluting.pollution = pollutionLevels[levelable.Level - 1];

        // Destroy children and add new model as current model
        foreach(Transform t in transform){
            Destroy(t.gameObject);
        }

        GameObject newGameObject = (GameObject)Instantiate(buildingLevelModels[levelable.Level - 1], transform.position, transform.rotation);
        newGameObject.transform.parent = transform;
    }

    private void OnBuildingDestroy(Damagable damagable){
    	levelable.LevelUp -= OnLevelUp;
    	damagable.BeforeDestroy -= OnBuildingDestroy;
        Destroy(gameObject, destroyDelay);
    }
}

