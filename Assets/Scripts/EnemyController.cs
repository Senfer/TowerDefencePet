using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject objective;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(objective.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
