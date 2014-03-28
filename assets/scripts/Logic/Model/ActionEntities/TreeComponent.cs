using Industree.Facade;
using Industree.Facade.Internal;
using Industree.Logic.StateManager;
using Industree.Time;
using Industree.Time.Internal;
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
	
	private LevelManager levelManager;
    private Damagable damagable;
    private Polluting polluting;
	
	public AudioClip levelUpSound;

	public Damagable Damagable { get { return damagable; } }
	
	private void Awake(){
        levelManager = new LevelManager(0);
		damagable = GetComponent<Damagable>();
		polluting = GetComponent<Polluting>();
        Timing.GetTimerFactory().GetTimer(timeBetweenClean).Tick += OnCleanTimerTick;
	}

	private void Start(){
		levelManager.LevelUp += OnLevelUp;
		damagable.BeforeDestroy += OnTreeDestroy;

        levelUpTimes = new float[minLevelUpTimes.Length];
		for(int i = 0; i < levelUpTimes.Length; i++){
			levelUpTimes[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}

        Timing.GetTimerFactory().GetTimer(levelUpTimes[0]).Tick += OnLevelUpTimerTick;
       
        polluting.pollution = -reducePollution[0];

        animation.Play(plantAnim);
        audio.PlayOneShot(levelUpSound);
	}

	private void OnLevelUp(int oldLevel, int newLevel){
        polluting.pollution = -reducePollution[oldLevel - 1];

		animation.Play(growAnim[oldLevel - 1]);		
		audio.PlayOneShot(levelUpSound);
	}

	private void OnTreeDestroy(Damagable damagable){
		Destroy(gameObject, 2);
        levelManager.LevelUp -= OnLevelUp;
        damagable.BeforeDestroy -= OnTreeDestroy;
	}

	private void OnCleanTimerTick(ITimer timer){
		player.IncreaseCredits(creditsPerSec[levelManager.Level]);
	}

    private void OnLevelUpTimerTick(ITimer timer)
    {
        levelManager.RaiseLevel();

        if (levelManager.Level < 4)
        {
            timer.Stop();
            Timing.GetTimerFactory().GetTimer(levelUpTimes[levelManager.Level]).Tick += OnLevelUpTimerTick;
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
        animation.PlayQueued(idleAnim[levelManager.Level], QueueMode.CompleteOthers);
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

