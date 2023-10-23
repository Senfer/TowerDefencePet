using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public event Action<GameObject> acquiredTarget = delegate { };

    public Collider attachedCollider;
    public float fireDelayS;

    private float fireTimer;
    private GameObject currentTarget;
    private List<GameObject> targetsInRange = new List<GameObject>();

    //public float effectRadius
    //{
    //    get
    //    {
    //        var sphere = attachedCollider as SphereCollider;
    //        if (sphere != null)
    //        {
    //            return sphere.radius;
    //        }

    //        return 0;
    //    }
    //}

    protected void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0.0f && targetsInRange.Count > 0)
        {
            UpdateCurrentTarget();
            // probably remove
            if (currentTarget == null)
            {
                return;
            }

            Debug.Log("Fire!");

            acquiredTarget(currentTarget);
            fireTimer = fireDelayS;
        }
    }

    private void UpdateCurrentTarget()
    {
        if (targetsInRange.Count > 0)
        {
            currentTarget = targetsInRange.First();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(currentTarget == other.gameObject)
            {
                currentTarget = null;
            }

            targetsInRange.Remove(other.gameObject);

            Debug.Log("Target removed");
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetsInRange.Add(other.gameObject);
            Debug.Log("Target added");
        }
    }
}
