using System;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public int health;

    public event Action<Damagable> destroyed = delegate { };

    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage taken: {damage}");
        health -= damage;
        if (health <= 0)
        {
            destroyed(this);
            Destroy(gameObject);
        }
    }
}

