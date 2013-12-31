using UnityEngine;

public class Hail :ActionEntity {

    [Range(0, 1)]
    public float damageProbability;

    private Planet planet;
    private Damaging damaging;
    private Damagable currentDamagable;

    private void Awake(){
        planet = GameObject.FindGameObjectWithTag(Tags.planet).GetComponent<Planet>();
        damaging = GetComponent<Damaging>();
    }

    private void Start(){
        GetComponent<SphericalMover>().moveSpeed *= Mathf.Sign(ActionDirection);
        transform.LookAt(planet.transform.position, Vector3.forward);
    }

    private void Update(){
        RaycastHit hit;
        if (Physics.Linecast(transform.position, planet.transform.position, out hit, Layers.Building)){

            Damagable hitDamagable = Utilities.GetMostOuterAncestor(hit.collider.transform).GetComponent<Damagable>();
            if (!hitDamagable.Destroyed && currentDamagable != hitDamagable){
        
                currentDamagable = hitDamagable;
                bool propabilityHits = UnityEngine.Random.Range(0f, 1f) <= damageProbability;
                if (propabilityHits){
                    damaging.CauseDamage(currentDamagable);
            	}
			}
        }
    }
}