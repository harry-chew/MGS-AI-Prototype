using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolWaypoints;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform player;
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
                SetTargetToChase(player);
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
            
        }
        currentTarget = patrolWaypoints[currentWaypoint];
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypoint].position);
    }

    public void Update()
    {
        if(currentState == EnemyState.Patrol)
        {
            if (CalculateDistance(transform.position, patrolWaypoints[currentWaypoint].position) <= 1f)
            {
                SetNextWaypoint();
            }
        }
        
        if(currentState == EnemyState.Chase)
        {
            if (CalculateDistance(transform.position, currentTarget.position) <= 5f)
            {
                navMeshAgent.SetDestination(currentTarget.position);
            }
            else if (CalculateDistance(transform.position, player.position) <= 1f)
            {
                ChangeEnemyState(EnemyState.Attack);
            }
            else
            {
                ChangeEnemyState(EnemyState.Patrol);
            }
        }


    }

    public float CalculateDistance(Vector3 v1, Vector3 v2)
    {
        return Vector3.Distance(v1, v2);
    }

    public void SetTargetToChase(Transform targetTransform)
    {
        this.currentTarget = targetTransform;
        navMeshAgent.SetDestination(currentTarget.position);
    }

}

public enum EnemyState
{
    Patrol,
    Chase,
    Attack
}