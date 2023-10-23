using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage taken: {damage}");
    }
}

