using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Targeter targeter;

    [SerializeField]
    public TowerData towerData;

    void Start()
    {
        targeter.acquiredTarget += OnAcquiredTarget;
    }

    void OnDestroy()
    {
        targeter.acquiredTarget -= OnAcquiredTarget;
    }

    private void OnAcquiredTarget(GameObject target)
    {
        LaunchProjectile(target);
    }

    private void LaunchProjectile(GameObject target)
    {
        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();

        projectile.Fire(transform.position + new Vector3(0, 1.3f), target);
    }
}
