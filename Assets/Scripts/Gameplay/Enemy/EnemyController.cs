using Assets.Scripts.Gameplay.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyStates State { get; protected set; }

    public NavMeshAgent Agent;
    public Damageable Damageable;
    public int Reward;
    public int Damage;

    public void Start()
    {
        Damageable.HealthDepleted += OnHealthDepleted;
    }    

    public void ReachObjective()
    {
        State = EnemyStates.ReachedObjective;
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (GameplayManager.InstanceExists)
        {
            GameplayManager.Instance.EnemyDestroyedCallback(this);
        }
    }

    private void OnHealthDepleted(Damageable _)
    {
        State = EnemyStates.Dead;        
    }
}
