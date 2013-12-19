using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Building:Interactive
{
    public int hitpoints;
    public bool alive = true;
    public int poullution;
    public int level = 1;
    public float[] minLevelUpTimes;
    public float[] maxLevelUpTimes;

    public delegate void LevelUpHandler(Building sender);
    public event LevelUpHandler LevelUp;

    private float lastTime = 0;
    private float dyingSpeed = 0;
    private float[] levelUpBuildingTime = {0,0,0};

	private Planet planet;
	
	private float levelUpTimer;
	
	public void Start(){
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		
		for(int i = 0; i < minLevelUpTimes.Length; i++){
	        levelUpBuildingTime[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}
		
       	levelUpTimer = 0f;

        LevelUp += OnLevelUp;
	}

    private void OnLevelUp(Building buildingToLevelUp)
    {
        level += 1;
        levelUpTimer = 0f;
        planet.levelUpBuilding(this);
    }
	
    public void Update()
    {
        if (alive)
        {
            pollute();
			
			levelUpTimer += Time.deltaTime;
			
			if (level <= levelUpBuildingTime.Length && levelUpTimer >= levelUpBuildingTime[level - 1]){
                LevelUp(this);
        	}
			
			
        }
        else
        {
            destroyMovement();
        }
    }
	
    public void takeDamage(int damage)
    {
        hitpoints -= damage;

        if (hitpoints <= 0)
        {
            hitpoints = 0;
            destroy();
        }
    }

    public void pollute()
    {
		
        //Pollute every second
        if (Time.time > lastTime + 1)
        {
            planet.pollution += poullution;
            lastTime = Time.time;
        }
        
    }

    public void destroy()
    {
        Destroy(gameObject, 3);
        alive = false;
    }

    public void destroyMovement()
    {
        dyingSpeed += 0.1f;
        transform.position -= transform.up*Time.deltaTime*dyingSpeed;
    }
}

