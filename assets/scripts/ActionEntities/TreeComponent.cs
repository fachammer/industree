using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// TODO: fix animations
// TODO: fix tree size
public class TreeComponent: MonoBehaviour {

    public int[] creditsPerSec = { 1, 2, 3 };
    public int[] reducePollution = { 1, 2, 3 };
    public float timeBetweenClean;
	
	public float[] minLevelUpTimes;
	public float[] maxLevelUpTimes;

    public String[] idleAnim;
    public String[] growAnim;

    public Player player;

    private float dieSpeed = 0;
    public Vector3 down;
	
	private Levelable levelable;
    private Damagable damagable;
    private Polluting polluting;
	
	public AudioClip soundLevelUp;

	public Damagable Damagable { get { return damagable; } }
	
	private void Awake(){
		levelable = GetComponent<Levelable>();
		damagable = GetComponent<Damagable>();
		polluting = GetComponent<Polluting>();
		Timer.AddTimerToGameObject(gameObject, timeBetweenClean, OnCleanTimerTick);
	}

	private void Start(){
		levelable.LevelUp += OnLevelUp;
		damagable.BeforeDestroy += OnTreeDestroy;

		for(int i = 0; i < levelable.levelUpTimes.Length; i++){
			levelable.levelUpTimes[i] = UnityEngine.Random.Range(minLevelUpTimes[i], maxLevelUpTimes[i]);
		}
	}

	private void OnLevelUp(Levelable levelable){
		// animation.Play(growAnim[levelable.Level]);		
		audio.PlayOneShot(soundLevelUp);

		polluting.pollution = -reducePollution[levelable.Level - 1];
	}

	private void OnTreeDestroy(Damagable damagable){
		Destroy(gameObject, 2);
        down = -transform.up;
        levelable.LevelUp -= OnLevelUp;
        damagable.BeforeDestroy -= OnTreeDestroy;
	}

	private void OnCleanTimerTick(Timer timer){
		player.IncreaseCredits(creditsPerSec[levelable.Level - 1]);
	}

    public void Update(){
        if (damagable.Destroyed){
            dieAnimation();
        }

        // Plays the animation after the last animation
        // animation.PlayQueued(idleAnim[levelable.Level - 1],QueueMode.CompleteOthers);
    }

    private void dieAnimation(){
        dieSpeed += 0.1f;

        if (transform.rotation == Quaternion.Euler(-90,0,0)){
            transform.position = down * Time.deltaTime * dieSpeed;
        }
        else{
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-90, 0, 0), dieSpeed);
        }
    }
}

