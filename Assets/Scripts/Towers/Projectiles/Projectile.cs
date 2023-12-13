using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float speed;
    public int damage;

    private bool _fired;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_fired)
        {
            return;
        }

        if (_target.IsDestroyed())
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed * Time.deltaTime);
        }
    }

    public void Fire(Vector3 startPoint, GameObject target)
    {
        transform.position = startPoint;
        _target = target;
        _fired = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Damageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
