using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {

	public int initialHitpoints;
	
    private int hitpoints;
    private bool destroyed;

    public int Hitpoints {
    	get { return hitpoints; }
    }

    public bool Destroyed {
    	get { return destroyed; }
    }

    public delegate void DamagedHandler(Damagable damagedDamagable, int damage);
    public delegate void DestroyedHandler(Damagable destroyedDamagable);

    public event DamagedHandler Damage = delegate(Damagable damagable, int damage) { };
    public event DestroyedHandler BeforeDestroy = delegate(Damagable damagable) { };

    public void Start() {
    	hitpoints = initialHitpoints;
    }

    public void TakeDamage(int damage)
    {
        hitpoints -= damage;
        Damage(this, damage);

        if(hitpoints <= 0){
            destroyed = true;
            BeforeDestroy(this);
        }
    }
}
