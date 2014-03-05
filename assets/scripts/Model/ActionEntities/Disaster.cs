using UnityEngine;

public class Disaster :MonoBehaviour {

    public float duration;

    private void Start(){
		audio.Play();

        Destroy(gameObject, duration);
    }
}
