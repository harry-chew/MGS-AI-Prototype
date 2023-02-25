using System;
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


    [SerializeField] private float attackTimer;
    [SerializeField] private float attackCooldown;
    [SerializeField] private bool canAttack;

    [SerializeField] private float attackRange;
    [SerializeField] private float chaseRange;

    [SerializeField] private GameObject exclamtionMark;
    
    private int currentWaypoint = 0;

    [SerializeField] private NavMeshAgent navMeshAgent;

    private void Start()
    {
        exclamtionMark.SetActive(false);
        canAttack = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeEnemyState(EnemyState.Patrol);
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        currentState = newState;

        switch(currentState)
        {
            case EnemyState.Patrol:
                exclamtionMark.SetActive(false);
                currentWaypoint = 0;
                SetNextWaypoint();
                break;
            case EnemyState.Chase:
                exclamtionMark.SetActive(true);
                SetTargetToChase(player);
                break;
            case EnemyState.Attack:
                AttackPlayer();
                ChangeEnemyState(EnemyState.Chase);
                break;
            default:
                break;
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        player.GetComponent<PlayerMove>().Damage(10);
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

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
        }

        if(attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackTimer = 0f;
        }

        if (currentState == EnemyState.Patrol)
        {
            if (CalculateDistance(transform.position, patrolWaypoints[currentWaypoint].position) <= 1f)
            {
                SetNextWaypoint();
            }
        }
        
        if(currentState == EnemyState.Chase)
        {
            float distanceToTarget = CalculateDistance(transform.position, currentTarget.position);
            if (distanceToTarget <= chaseRange && distanceToTarget >= attackRange)
            {
                navMeshAgent.SetDestination(currentTarget.position);
            }
            if (distanceToTarget <= attackRange)
            {
                if(canAttack) ChangeEnemyState(EnemyState.Attack);
            }
            if(distanceToTarget >= chaseRange)
            {
                ChangeEnemyState(EnemyState.Patrol);
            }
        }
        
        if(currentState == EnemyState.Attack)
        {
            if (CalculateDistance(transform.position, player.position) >= attackRange)
            {
                ChangeEnemyState(EnemyState.Chase);
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