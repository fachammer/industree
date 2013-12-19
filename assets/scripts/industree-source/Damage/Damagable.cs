using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {

    public int hitpoints;

    public delegate void DamagedHandler(Damagable damagedDamagable, int damage);
    public delegate void DestroyedHandler(Damagable destroyedDamagable);

    public event DamagedHandler Damaged;
    public event DestroyedHandler Destroyed;

    public void Start()
    {
        Damaged += OnDamaged;
    }

    private void OnDamaged(Damagable damagedDamagable, int damage)
    {
        hitpoints -= damage;

        if (hitpoints <= 0)
        {
            Destroyed(this);
        }
    }

    public void TakeDamage(int damage)
    {
        Damaged(this, damage);
    }
}
