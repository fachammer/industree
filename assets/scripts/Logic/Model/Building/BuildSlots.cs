using UnityEngine;
using System.Collections;

public class BuildSlots : MonoBehaviour {

	[Range(0, 360)] public float minWorldRangeAngle;
	[Range(0, 360)] public float maxWorldRangeAngle;
	public float worldRadius;
	public int slotCount;

	private Vector3 worldCenter;
	private Vector3[] buildPositions;
	private GameObject[] buildSlotGameObjects;

	public Vector3[] BuildPositions { get { return buildPositions; } }
	public GameObject[] BuildSlotGameObjects { get { return buildSlotGameObjects; } }

	private void Awake(){
		worldCenter = GameObject.FindGameObjectWithTag(Tags.planet).transform.position;
		buildPositions = new Vector3[slotCount];
		buildSlotGameObjects = new GameObject[slotCount];

		float minWorldRangeAngleRad = minWorldRangeAngle * Mathf.Deg2Rad;
		float maxWorldRangeAngleRad = maxWorldRangeAngle * Mathf.Deg2Rad;
		float worldRangeAngle = maxWorldRangeAngleRad - minWorldRangeAngleRad;
		float angleStep = worldRangeAngle / slotCount;
		
        for (int i = 0; i < slotCount; i++) {			
			float x = Mathf.Cos(i * angleStep + minWorldRangeAngleRad) * worldRadius;
			float y = Mathf.Sin(i * angleStep + minWorldRangeAngleRad) * worldRadius;
            buildPositions[i] = new Vector3(x, y, 0);
        }
	}

	public bool IsBuildSlotOccupied(int slotIndex){
		return buildSlotGameObjects[slotIndex] != null;
	}

	public void PlaceGameObjectOnBuildSlot(GameObject gameObjectToPlace, int slotIndex){
		buildSlotGameObjects[slotIndex] = gameObjectToPlace;
		gameObjectToPlace.transform.position = buildPositions[slotIndex]; 
		gameObjectToPlace.transform.LookAt(worldCenter, Vector3.forward);
	}
}
