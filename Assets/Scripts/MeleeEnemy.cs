using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Vector3 lastPosition = Vector3.zero;
    public float movementSpeed = 6.0f;
    public Animator _animator;
    public Transform[] waypoints;
    private int currentWaypoint;
    public float viewRadius = 5;
    public float attackRadius = 3;
    public int attackCoolDown = 2;
    public int walkCoolDown = 5;
    public Transform target;
    private float attackTimeStamp;
    private float walkTimeStamp;


    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    private void Start() {
        lastPosition = transform.position;
    }

    private void Update() {
        float currentSpeed = Vector3.Distance(transform.position ,lastPosition);
        Debug.Log(currentSpeed);
        _animator.SetFloat("speed", currentSpeed * movementSpeed);
        lastPosition = transform.position;
        _animator.SetBool("isShutDown", false);

        float distance = Vector3.Distance(target.position, transform.position);
        float level = Mathf.Abs((target.position - transform.position).y);
        if (distance <= viewRadius && level < 1) {
            FaceTarget(target);
            if (distance >= 1) {
                transform.position = Vector3.MoveTowards(transform.position, target.position,
                    Time.deltaTime * movementSpeed);
            }
            if (distance <= attackRadius && Time.time >= attackTimeStamp) {
                _animator.SetTrigger("scanTrigger");
                attackTimeStamp = Time.time + attackCoolDown;
                //TODO
               
            }
        } else if (Time.time >= walkTimeStamp) {
            if (currentWaypoint == waypoints.Length) {
                currentWaypoint = 0;
            }

            FaceTarget(waypoints[currentWaypoint]);
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[currentWaypoint].transform.position, Time.deltaTime * movementSpeed);
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 0.2) {
                walkTimeStamp = Time.time + walkCoolDown;
                currentWaypoint++;
            }
        }
    }
    void FaceTarget(Transform currentTarget) {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
