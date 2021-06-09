using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class BotMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _groundLayer, _playerLayer;
    [SerializeField] private Vector3 _walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;
    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private bool alreadyAttacked;
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSigthRange, playerInAttackRange;
    [SerializeField] float _speed = 2.5f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
   // [SerializeField] private Animator _animator;
    private float turnSmooth;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    void Update()
    {
       // _agent.ResetPath();
        _agent.SetDestination(_target.position);
        //velocity
        // float velocity = _agent.velocity.magnitude/_agent.speed;

        // _animator.SetFloat("zVelocity", velocity, 0.3f, Time.deltaTime);
        // _animator.SetFloat("xVelocity", velocity, 0.3f, Time.deltaTime);

        //movement
        // playerInSigthRange = Physics.CheckSphere(transform.position, sightRange, _playerLayer);
        // playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, _playerLayer);
        // //if (!playerInAttackRange && !playerInSigthRange) Patrol();
        // if (playerInAttackRange && !playerInSigthRange) Chase();
        // if (playerInAttackRange && playerInSigthRange) Attack();
        // float runSpeed = 1;
        //

    }

   private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _groundLayer))
            walkPointSet = true;
    }

    private void Chase()
    {
        _agent.SetDestination(_target.position);
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_target);

        if (!alreadyAttacked)
        {
            ///Attack code here
            //TODO ATTACK
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

   
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}