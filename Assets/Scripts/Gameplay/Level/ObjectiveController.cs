using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            GameplayManager.Instance.IncreaseScore(-10);
        }
    }
}
