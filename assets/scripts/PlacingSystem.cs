
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacingSystem
{
	private List<GameObject> treesAndBuildings;
	private List<Vector3> sphericalPositions;
	
	private int placeCount;
	
	private int maxBuildingsCountLeft;
	private int maxBuildingsCountRight;
	
	private int maxTreesCountLeft;	
	private int maxTreesCountRight;
	
	public PlacingSystem(float minWorldRangeAngle, float maxWorldRangeAngle, int placeCount, float worldRadius){
		treesAndBuildings = new List<GameObject>();
		sphericalPositions = new List<Vector3>();
		
		this.placeCount = placeCount;
		
		float minWorldRangeAngleRad = minWorldRangeAngle * Mathf.Deg2Rad;
		float maxWorldRangeAngleRad = maxWorldRangeAngle * Mathf.Deg2Rad;
		float worldRangeAngle = maxWorldRangeAngleRad - minWorldRangeAngleRad;
		float angleStep = worldRangeAngle / placeCount;
		
		Random.seed = System.Environment.TickCount ;
		
        for (int i = 1; i <= placeCount; i++) {
			treesAndBuildings.Add(null);
			
			float x = Mathf.Cos(i * angleStep + minWorldRangeAngleRad) * worldRadius;
			float y = Mathf.Sin(i * angleStep + minWorldRangeAngleRad) * worldRadius;
            sphericalPositions.Add(new Vector3(x, y, 0));
        }
		
		maxBuildingsCountLeft = maxTreesCountLeft = (int)(placeCount / 2);
		maxBuildingsCountRight = maxTreesCountRight = (int)(placeCount / 2) + 1;
	}
	
	public GameObject placeNewIndustryBuilding(GameObject industryBuildingModel) { 
		int buildingIndex = getNextBuildingIndex();
		GameObject replacedGameObject;
		
		GameObject newGameObject = placeGameObject(industryBuildingModel, buildingIndex, out replacedGameObject);
		rotateIndustryBuildingRandomly(newGameObject);
		
		return replacedGameObject;
    }
	
	private void rotateIndustryBuildingRandomly(GameObject industryBuilding){
		int randomRotationIndex = Random.Range(0, 2);
		industryBuilding.transform.Rotate(0, randomRotationIndex * 180, 0);
	}
	
	public void placeNewTree(Player player, GameObject treeModel){
		
		int treeIndex = getNextTreeIndex(player.side);
		GameObject replacedGameObject;
		
		GameObject newGameObject = placeGameObject(treeModel, treeIndex, out replacedGameObject);
		
		newGameObject.transform.Rotate(new Vector3(-90, 0, 0));
		float randomScale = Random.Range (0.8f, 1.0f);
		newGameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		
		newGameObject.GetComponent<TreeComponent>().player = player;
	}
	
	public bool canPlaceBuilding(){
		
		bool canPlaceRight = false;
		bool canPlaceLeft = false;
		
		for(int i = maxBuildingsCountLeft; i >= 0; i--){
			if(canPlaceBuildingOnIndex(i)){
				canPlaceRight = true;
			}
		}
		for(int i = maxBuildingsCountRight; i < placeCount; i++){
			if(canPlaceBuildingOnIndex(i)){
				canPlaceLeft = true;
			}
		}
		
		return canPlaceLeft && canPlaceRight;
	}
	
	private bool canPlaceBuildingOnIndex(int index){
		return canPlaceTreeOnIndex(index) || treesAndBuildings[index].GetComponent<TreeComponent>() != null;
	}
	
	private bool canPlaceTreeOnIndex(int index){
		return treesAndBuildings[index] == null || (treesAndBuildings[index].GetComponent<Building>() != null &&
				treesAndBuildings[index].GetComponent<Building>().Damagable.Destroyed);
	}
	
	public bool canPlaceTree(Player.Side side){		
		return getNextTreeIndex(side) != -1;
	}
	
	private GameObject placeGameObject(GameObject model, int placeIndex, out GameObject replacedObject){

		GameObject newGameObject = (GameObject) MonoBehaviour.Instantiate(model, sphericalPositions[placeIndex], Quaternion.identity);
		
		Vector3 worldCenter = Vector3.zero;
		newGameObject.transform.LookAt(worldCenter, Vector3.right);
		// newGameObject.transform.Rotate(new Vector3(0, 90, 0));
		newGameObject.transform.Rotate(new Vector3(0, 0, 90), Space.World);
		
		replacedObject = treesAndBuildings[placeIndex];
		treesAndBuildings[placeIndex] = newGameObject;
		
		return newGameObject;
	}
	
	private int getNextBuildingIndex(){
		int direction = Random.Range(0, 2);
		
		// go to the right
		if(direction == 1){
			
			for(int i = maxBuildingsCountRight; i >= 0; i--){
				if(canPlaceBuildingOnIndex(i)){
					return i;
				}
			}
		}
		// go to the left
		else{
			for(int i = maxBuildingsCountLeft; i < placeCount; i++){
				if(canPlaceBuildingOnIndex(i)){
					return i;
				}
			}
		}
		
		// should never get called
		return int.MaxValue;
	}
	
	private int getNextTreeIndex(Player.Side side){
		if(side == Player.Side.left){
			
			for(int i = placeCount - 1; i >= maxTreesCountLeft; i--){
				if(canPlaceTreeOnIndex(i)){
					return i;
				}
			}
		}
		else{
			for(int i = 0; i < maxTreesCountRight; i++){
				if(canPlaceTreeOnIndex(i)){
					return i;
				}
			}
		}
		
		// can't place any trees in the given direction
		return -1;
	}
	
	public void levelUpBuilding(Building building, GameObject newBuildingModel){  
		
		if(building == null){
			Debug.Log("building is null");
		}
		int buildingIndex = treesAndBuildings.FindIndex(delegate(GameObject obj) {
			if(obj != null){
				return obj == building.gameObject;
			}
			return false;
		});
		
		GameObject replacedObj;
		
		GameObject newGameObject = placeGameObject(newBuildingModel, buildingIndex, out replacedObj);

		rotateIndustryBuildingRandomly(newGameObject);
		
		MonoBehaviour.Destroy(replacedObj);
	}
}
