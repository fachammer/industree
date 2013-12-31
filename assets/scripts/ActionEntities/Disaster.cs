using UnityEngine;

public class Disaster :MonoBehaviour {

    public float duration;

    public virtual void Start(){
		audio.Play();

        Destroy(gameObject, duration);
    }
}
