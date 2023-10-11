using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject spawningInstance;
    public GameObject spawningInstanceTarget;

    // Start is called before the first frame update
    void Start()
    {
        spawningInstance.GetComponent<EnemyController>().objective = spawningInstanceTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {           
            Instantiate(spawningInstance);
        }
    }
}
