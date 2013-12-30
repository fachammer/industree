using UnityEngine;
using System.Collections;

public class SphericalMover : MonoBehaviour {

	public float moveSpeed;

	private Planet planet;

	private void Awake () {
		planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
	}

	private void Update () {
		transform.RotateAround(planet.transform.position, Vector3.back, moveSpeed * Time.deltaTime);
	}
}
