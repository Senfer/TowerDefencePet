using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().ReachObjective();
        }
    }
}
