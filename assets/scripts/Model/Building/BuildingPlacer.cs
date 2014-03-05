using UnityEngine;
using System.Collections;
using assets.scripts.Miscellaneous;

public class BuildingPlacer : MonoBehaviour {

	public float buildInterval;
	public GameObject buildingModel;

	private BuildSlots buildSlots;

	private void Awake () {
		buildSlots = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<BuildSlots>();
		Timer.AddTimerToGameObject(gameObject, buildInterval, OnBuildTimerTick);
	}

	private void OnBuildTimerTick(Timer timer){
		int nextBuildingIndex = GetNextBuildingIndex();
		if(nextBuildingIndex != -1){
			PlaceNewBuildingOnIndex(nextBuildingIndex);
		}
	}

	private int GetNextBuildingIndex(){
		bool nextBuildingIsPlacedLeft = Utilities.RandomBool();
		int searchDirection = nextBuildingIsPlacedLeft ? -1 : 1;

		for(int i = (int)(buildSlots.slotCount / 2); nextBuildingIsPlacedLeft ? i >= 0 : i < buildSlots.slotCount; i += searchDirection){
			if(CanPlaceBuildingOnIndex(i)){
				return i;
			}
		}

		return -1;
	}

	private bool CanPlaceBuildingOnIndex(int index){
		return !buildSlots.IsBuildSlotOccupied(index) || buildSlots.BuildSlotGameObjects[index].tag == Tags.tree;
	}

	private void PlaceNewBuildingOnIndex(int slotIndex){
		GameObject gameObjectToReplace = buildSlots.BuildSlotGameObjects[slotIndex];

		if(gameObjectToReplace != null && gameObjectToReplace.tag == Tags.tree){
			Damagable replacedObjectDamagable = gameObjectToReplace.GetComponent<Damagable>();
			replacedObjectDamagable.TakeDamage(replacedObjectDamagable.Hitpoints);
		}

		GameObject buildingClone = (GameObject) Instantiate(buildingModel);
		buildSlots.PlaceGameObjectOnBuildSlot(buildingClone, slotIndex);
		buildingClone.transform.Rotate(-90, 0, 0);
		RotateBuildingRandomly(buildingClone);
	}

	private void RotateBuildingRandomly(GameObject building){
		int randomRotationIndex = Random.Range(0, 2);
		building.transform.Rotate(0, randomRotationIndex * 180, 0);
	}
}
