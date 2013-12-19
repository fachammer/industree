using UnityEngine;
using System.Collections;

public class Damaging : MonoBehaviour {

    public int damage;

    public void Damage(Damagable damagable)
    {
        damagable.TakeDamage(damage);
    }
}
