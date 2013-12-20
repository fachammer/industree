using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TreeComponent:Interactive
{
    public int[] creditsPerSec = { 1, 2, 3 };
    public int[] reducePollution = { 1, 2, 3 };
    public float[] levelUpTime = {0,0};
	
	public float[] minLevelUpTimes;
	public float[] maxLevelUpTimes;

    public String[] idlAnim;
    public String[] growAnim;

    public int level = 0;
    public bool alive = true;
    public Player player;

    private float lastTime = 0;
    private float startTime;
    private float dieSpeed=0;
    public Vector3 down;
	
	private Planet planet;
	
	public AudioClip soundLevelUp;
	

    public void Start()
	{
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
        levelUpTime[0] = UnityEngine.Random.Range(minLevelUpTimes[0], maxLevelUpTimes[0]);
        levelUpTime[1] = levelUpTime[0] + UnityEngine.Random.Range(minLevelUpTimes[1], maxLevelUpTimes[1]);

        startTime = Time.time;

        levelUp();
    }


    public void Update()
    {
        if (Time.time > lastTime + 1)
        {
            createCredits();
            cleanAir();

            lastTime = Time.time;
        }

        if (level != 3 && Time.time - startTime > levelUpTime[level - 1])
        {
            levelUp();
        }

        if (!alive)
        {
            dieAnimation();
        }

        //Plays the animation after the last animatin
        // animation.PlayQueued(idlAnim[level - 1],QueueMode.CompleteOthers);
        
    }

    public void createCredits()
    {
        player.credits += creditsPerSec[level-1];
    }

    public void cleanAir()
    {
        planet.GetComponent<Pollutable>().currentPollution -= reducePollution[level-1];
    }

    public void levelUp()
    {
        // animation.Play(growAnim[level]);
        level += 1;
		
		audio.PlayOneShot(soundLevelUp);
    }

    public void die()
    {
        alive = false;
        Destroy(gameObject, 2);
        down = -transform.up;
    }

    public void dieAnimation()
    {
        dieSpeed += 0.1f;

        if (this.transform.rotation == Quaternion.Euler(-90,0,0))
        {
            transform.position = down * Time.deltaTime * dieSpeed;
        }
		
		
        else
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-90, 0, 0), dieSpeed);
        }

        
    }
	
	public override bool performAction(Player player, float castDirection){
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
		return planet.placeTree(player);
	}
}

