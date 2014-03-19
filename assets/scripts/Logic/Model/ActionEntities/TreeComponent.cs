using Industree.Facade;
using Industree.Facade.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// TODO: fix animations
public class TreeComponent: MonoBehaviour {

    public int[] creditsPerSec = { 1, 2, 3 };
    public int[] reducePollution = { 1, 2, 3 };
    public float timeBetweenClean;
	
	public float[] minLevelUpTimes;
	public float[] maxLevelUpTimes;

    public String plantAnim;
    public String[] idleAnim;
    public String[] growAnim;

    public IPlayer player;

    private float[] levelUpTimes;
    private float dieSpeed = 0f;
	
	private Levelable levelable;
    private Damagable damagable;
    private Polluting polluting;
	
	public AudioClip levelUpSound;

	public Damagable Damagable { get { return damagable; } }
	
	private void Awake(){
		levelable = GetComponent<Levelable>();
		damagable = GetComponent<Damagable>();
		polluting = GetComponent<Polluting>();
		Timer.Start(timeBetweenClean, OnCleanTimerTick);
	}

	private void Start(){
		levelable.LeveledUp += OnLevelUp;
		damagable.BeforeDestroy += OnTreeDestroy;

        levelUpTimes = new float[minLevelUpTimes.Length];
		for(int i = 0; i < levelUpTimes.Length; i++){
			levelUpTimes[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}

        Timer.Start(levelUpTimes[0], OnLevelUpTimerTick);

        polluting.pollution = -reducePollution[0];

        animation.Play(plantAnim);
        audio.PlayOneShot(levelUpSound);
	}

	private void OnLevelUp(Levelable levelable){
        polluting.pollution = -reducePollution[levelable.Level - 2];

		animation.Play(growAnim[levelable.Level - 2]);		
		audio.PlayOneShot(levelUpSound);
	}

	private void OnTreeDestroy(Damagable damagable){
		Destroy(gameObject, 2);
        levelable.LeveledUp -= OnLevelUp;
        damagable.BeforeDestroy -= OnTreeDestroy;
	}

	private void OnCleanTimerTick(Timer timer){
		player.IncreaseCredits(creditsPerSec[levelable.Level - 1]);
	}

    private void OnLevelUpTimerTick(Timer timer)
    {
        levelable.LevelUp();

        if (levelable.Level < levelable.maxLevel)
        {
            timer.interval = levelUpTimes[levelable.Level - 1];
        }
        else
        {
            timer.Stop();
        }
    }

    public void Update(){
        if (damagable.Destroyed){
            AnimateDeath();
        }

        // Plays the animation after the last animation
        animation.PlayQueued(idleAnim[levelable.Level - 1], QueueMode.CompleteOthers);
    }

    private void AnimateDeath(){
        dieSpeed += 0.1f;

        if (transform.rotation == Quaternion.Euler(-90,0,0)){
            transform.position = -transform.up * Time.deltaTime * dieSpeed;
        }
        else{
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-90, 0, 0), dieSpeed);
        }
    }
}

