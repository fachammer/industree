using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Planet : MonoBehaviour {
	
	[Range(0, 360)]
	public float minWorldRangeAngle;
	[Range(0, 360)]
	public float maxWorldRangeAngle;
	
	public GameObject treeModel;
	
	public GameObject industryBuildingModel;
	
	public float worldRadius;
	public int placeCount;
    public float timeBetweenBuild;
	
	public float air;
	
	private PlacingSystem placingSystem;
	
	private float buildTimer = 0.0f;
	
	public Light sun;
	public Color lightColor_clean;
	public Color lightColor_dirty;
	
	private bool gameWin = false;
	
	public AudioClip soundVictory;
	public AudioClip soundDefeated;

	[HideInInspector]
	public Pollutable pollutable;

	private GameController gameController;
   
	// Use this for initialization
	void Start () {
		pollutable = GetComponent<Pollutable>();
		gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		gameController.GameEnd += OnGameEnd;

		Screen.lockCursor = true;
		Screen.showCursor = false;
		
		placingSystem = new PlacingSystem(minWorldRangeAngle, maxWorldRangeAngle, placeCount, worldRadius);
	}

	void OnGameEnd(bool win){
		gameWin = win;

		if(gameWin){
			audio.PlayOneShot(soundVictory);
		}
		else {
			audio.PlayOneShot(soundDefeated);
		}
	}

    // Update is called once per frame
    void Update(){
		if(gameController.GameStarted){
	        buildTimer += Time.deltaTime;

			if(buildTimer >= timeBetweenBuild){
				
				if(placingSystem.canPlaceBuilding()){
					GameObject replacedObject = placingSystem.placeNewIndustryBuilding(industryBuildingModel);
					
					if(replacedObject != null && replacedObject.tag == Tags.tree){
						TreeComponent replacedTree = replacedObject.GetComponent<TreeComponent>();
						replacedTree.Damagable.TakeDamage(replacedTree.Damagable.Hitpoints);
					}

					buildTimer = 0f;
				}
			}
		}
		
		setAirPollution();
		
	}
	
	public bool placeTree(Player player){
		bool canPlaceTree = placingSystem.canPlaceTree(player.side);
		if(canPlaceTree){
			placingSystem.placeNewTree(player, treeModel);
		}
		
		return canPlaceTree;
	}

	public bool CanPlaceTree(Player player){
		return placingSystem.canPlaceTree(player.side);
	}
	
	public void setAirPollution()
	{
		sun.color = Color.Lerp(lightColor_clean,lightColor_dirty, pollutable.currentPollution / air);
	}
}