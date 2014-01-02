using UnityEngine;
using System.Collections;

public class InstantiateTornadoAction : InstantiateOnPositionAction {

    public float xRange;

	protected override Vector3 GetInitialActionEntityPosition(Player player, float actionDirection){
		GameObject planet = GameObject.FindGameObjectWithTag(Tags.planet);
        Vector3 position = new Vector3(UnityEngine.Random.Range(-xRange, xRange), 60, planet.transform.position.z);
        RaycastHit hit;

        if(Physics.Raycast(position, Vector3.down, out hit, 100, ~Layers.Building)){
            return hit.point;
        }

        // should never happen
        return Vector3.zero;
	}
}
