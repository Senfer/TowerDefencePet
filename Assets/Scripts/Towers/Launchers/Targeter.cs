using Assets.Scripts.Gameplay.States;
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

    private void Start()
    {
        GameplayManager.Instance.GameplayStateChanged += OnGameplayStateChanged;
    }

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

            other.GetComponent<Damagable>().destroyed -= OnEnemyDestroyed;
            targetsInRange.Remove(other.gameObject);

            Debug.Log("Target removed");
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetsInRange.Add(other.gameObject);
            other.GetComponent<Damagable>().destroyed += OnEnemyDestroyed;
            Debug.Log("Target added");
        }
    }

    private void OnEnemyDestroyed(Damagable damagable)
    {
        var enemy = targetsInRange.First(x => x.GetComponent<Damagable>() == damagable);
        if (enemy != null)
        {
            targetsInRange.Remove(enemy);
        }
    }

    private void OnGameplayStateChanged(GameplayState previousState, GameplayState currentState)
    {
        if (currentState == GameplayState.WavesIncoming)
        {
            attachedCollider.enabled = true;
        }
    }
}
