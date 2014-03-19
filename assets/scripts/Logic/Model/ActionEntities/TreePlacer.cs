using UnityEngine;
using System.Collections;
using Industree.Model;
using Industree.Facade;

public class TreePlacer : MonoBehaviour {

	public GameObject treeModel;

	private BuildSlots buildSlots;

	private void Awake(){
		buildSlots = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<BuildSlots>();
	}

	public TreeComponent PlaceTree(IPlayer player){
		int nextTreeIndex = GetNextTreeIndex(player.Index);

		GameObject treeClone = (GameObject) Instantiate(treeModel);
		buildSlots.PlaceGameObjectOnBuildSlot(treeClone, nextTreeIndex);
		treeClone.transform.Rotate(180, 0, 0);
		return treeClone.GetComponent<TreeComponent>();
	}

	private int GetNextTreeIndex(int playerIndex){
		int startIndex = buildSlots.slotCount - 1;
		int searchDirection = -1;
		if(playerIndex == 1){
			startIndex = 0;
			searchDirection = 1;
		}

		for(int i = startIndex; 
			searchDirection == 1 ? i < (buildSlots.slotCount / 2) : i >= (buildSlots.slotCount / 2) ; 
			i += searchDirection){
			if(!buildSlots.IsBuildSlotOccupied(i)){
				return i;
			}
		}

		return -1;
	}

	public bool CanPlaceTree(IPlayer player){
        return GetNextTreeIndex(player.Index) != -1;
	}

	private bool CanPlaceTreeOnIndex(int index){
		return buildSlots.IsBuildSlotOccupied(index);
	}

	private void RotateTreeRandomly(GameObject tree){
		int randomRotation = Random.Range(0, 360);
		tree.transform.Rotate(0, randomRotation, 0);
	}
}
