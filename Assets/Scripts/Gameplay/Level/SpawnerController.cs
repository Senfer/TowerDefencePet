using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject SpawningInstanceTarget;

    public void SpawnEntity(EnemyController spawnEntity)
    {
        var enemy = Instantiate(spawnEntity, transform.position, transform.rotation);
        enemy.Agent.SetDestination(SpawningInstanceTarget.transform.position);
    }
}
