using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {

	public int initialHitpoints;
	
    private int hitpoints;
    private bool destroyed;

    public int Hitpoints { 
        get { return hitpoints; }
        set { hitpoints = value; }
    }
    public bool Destroyed { get { return destroyed; } }

    public delegate void DamageHandler(Damagable damagedDamagable, int damage);
    public delegate void DestroyHandler(Damagable destroyedDamagable);

    public event DamageHandler Damage = delegate(Damagable damagable, int damage) { };
    public event DestroyHandler BeforeDestroy = delegate(Damagable damagable) { };

    public void Start() {
    	hitpoints = initialHitpoints;
    }

    public void TakeDamage(int damage)
    {
        hitpoints -= damage;
        Damage(this, damage);

        if(hitpoints <= 0){
            BeforeDestroy(this);
            destroyed = true;
        }
    }
}
