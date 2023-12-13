using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public int health;

    public event Action<Damageable> HealthDepleted = delegate { };

    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage taken: {damage}");
        health -= damage;
        if (health <= 0)
        {
            HealthDepleted(this);
            Destroy(gameObject);
        }
    }
}

