using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolWaypoints;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float distanceToWaypoint;
    [SerializeField] private EnemyState currentState;
    
    private int currentWaypoint = 0;

    [SerializeField] private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeEnemyState(EnemyState.Patrol);
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        currentState = newState;

        switch(currentState)
        {
            case EnemyState.Patrol:
                currentWaypoint = 0;
                SetNextWaypoint();
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }

    public void SetNextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= patrolWaypoints.Length)
        {
            currentWaypoint = 0;
            currentTarget = patrolWaypoints[currentWaypoint];
        }
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypoint].position);
    }

    public void Update()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, patrolWaypoints[currentWaypoint].position);
        if (distanceToWaypoint <= 1f) 
        {
            SetNextWaypoint();
        } 
    }

    public void SetTargetToChase(Transform targetTransform)
    {
        this.currentTarget = targetTransform;
    }
}

public enum EnemyState
{
    Patrol,
    Chase,
    Attack
}