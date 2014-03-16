using assets.scripts.Miscellaneous;
using UnityEngine;

public class Thunderstorm :ActionEntity {  

    public GameObject lightning;
    public float lightningDuration;
    public GameObject lightningSpark;
	public AudioClip lightningSound;

    private GameObject planet;
    private Damaging damaging;
    private bool hitBuilding = false;

    private void Awake(){
        planet = GameObject.FindGameObjectWithTag(Tags.planet);
        damaging = GetComponent<Damaging>();
    }

    private void Start(){
        GetComponent<SphericalMover>().moveSpeed *= Mathf.Sign(ActionDirection);
        transform.LookAt(planet.transform.position, Vector3.forward);
    }

    private void Update(){
        RaycastHit hit;
        if (!hitBuilding && Physics.Linecast(transform.position, planet.transform.position, out hit, Layers.Building))
        {
            Damagable hitDamagable = Utilities.GetMostOuterAncestor(hit.collider.transform).GetComponent<Damagable>();
			if(!hitDamagable.Destroyed)
			{
                damaging.CauseDamage(hitDamagable);
	            SendLightning(hit.point);
	            hitBuilding = true;
			}
        }
    }

    private void SendLightning(Vector3 position){
        GameObject lightningInstance = (GameObject)Instantiate(lightning, transform.position, transform.rotation);
        Instantiate(lightningSpark, position, Quaternion.identity);
		Destroy(lightningInstance, lightningDuration);
		
		audio.PlayOneShot(lightningSound);
    }
}
