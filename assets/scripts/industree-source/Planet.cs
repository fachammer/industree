using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Planet : MonoBehaviour {
	
	[Range(0, 360)]
	public float minWorldRangeAngle;
	[Range(0, 360)]
	public float maxWorldRangeAngle;
	
	public GameObject treeModel;
	
	public GameObject[] industryBuildingModels;
	
	public float worldRadius;
	public int placeCount;
    public float timeBetweenBuild;
	
	public float pollution;
	public float air =100;
	
	private PlacingSystem placingSystem;
	
	float buildTimer = 0.0f;
	float buildingLevelUpTimer = 0f;
	
	public float minTimeBetweenLevelUp;
	public float maxTimeBetweenLevelUp;
	private float timeBetweenLevelUp;

    public Vector2 bilanceSize;
    private Rect bilanceRect;
    private Rect pollutionRect;
	public Texture2D bilanceHeader;
	
    private Texture2D pollutionTex;
    private Texture2D bilanceTex;
	
	public Light sun;
	public Color lightColor_clean;
	public Color lightColor_dirty;

    public Texture2D winDialog;
    public Texture2D loseDialog;
	
	public bool started = false;
	public bool gameEnded = false;
	
	public AudioClip soundVictory;
	public AudioClip soundDefeated;
   
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Screen.showCursor = false;
		
		placingSystem = new PlacingSystem(minWorldRangeAngle, maxWorldRangeAngle, placeCount, worldRadius);

        bilanceRect = new Rect((Screen.width-bilanceSize.x)/2, 35,bilanceSize.x,bilanceSize.y);
        pollutionRect = new Rect((Screen.width - bilanceSize.x) / 2, 35, bilanceSize.x, bilanceSize.y);

        bilanceTex = new Texture2D(1, 1);
        pollutionTex = new Texture2D(1, 1);

        bilanceTex.SetPixel(1, 1, new Color(221f/255f, 255f/255f, 255f/255f));
        pollutionTex.SetPixel(1, 1, new Color(0.3f, 0.3f, 0.18f));

        bilanceTex.Apply();
        pollutionTex.Apply();
		
		timeBetweenLevelUp = Random.Range (minTimeBetweenLevelUp, maxTimeBetweenLevelUp);
		
		
	}

    // Update is called once per frame
    void Update(){
		if(started){
	        buildTimer += Time.deltaTime;
			buildingLevelUpTimer += Time.deltaTime;
			
			if(buildTimer >= timeBetweenBuild){
				
				if(placingSystem.canPlaceBuilding()){
					GameObject replacedObject = placingSystem.placeNewIndustryBuilding(industryBuildingModels[0]);
					
					if(replacedObject != null && replacedObject.tag == Tags.tree){
						replacedObject.GetComponent<TreeComponent>().die();
					}
					buildTimer = 0f;
				}
				else {
					// Game lost
				}
			}
			
			if(buildingLevelUpTimer >= timeBetweenLevelUp){
				//placingSystem.levelUpBuilding();
				
				timeBetweenLevelUp = Random.Range(minTimeBetweenLevelUp, maxTimeBetweenLevelUp);
				buildingLevelUpTimer = 0f;
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

    public void OnGUI()
    {
        GUI.DrawTexture(bilanceRect, bilanceTex, ScaleMode.StretchToFill);

		pollution = Mathf.Clamp(pollution,0,air);
        pollutionRect.width = bilanceRect.width * pollution / air;

        GUI.DrawTexture(pollutionRect, pollutionTex, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect((Screen.width-512)/2+2,0,512,128),bilanceHeader);

        if (pollution == air)
		{           
			if(!gameEnded)
				audio.PlayOneShot(soundDefeated);
			
			 showEndDialog(loseDialog);
		}
        if (pollution == 0)
		{            
			if(!gameEnded)
				audio.PlayOneShot(soundVictory);
			
			showEndDialog(winDialog);
		}
            
    }
	
	public void levelUpBuilding(Building building){
		placingSystem.levelUpBuilding(building, industryBuildingModels[building.level - 1]);
	}
	
	public void setAirPollution()
	{
		sun.color = Color.Lerp(lightColor_clean,lightColor_dirty,pollution / air);
	}

    public void showEndDialog(Texture2D message)
    {
		gameEnded = true;
        Time.timeScale = 0f;
        GUI.DrawTexture(new Rect((Screen.width-message.width)/2,200,message.width,message.height),message);

        if (Input.GetKeyDown(KeyCode.Q)){
            Application.LoadLevel(0);
        }
		else if(Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel(1);
		}

    }
}