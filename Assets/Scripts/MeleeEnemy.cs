using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Vector3 lastPosition = Vector3.zero;
    public float movementSpeed = 6.0f;

    public Animator Animator;

    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float viewRadius = 5;
    public float attackRadius = 3;
    public int attackCoolDown = 2;
    public int walkCoolDown = 5;
    public Transform target;
    private float attackTimeStamp = 0f;
    private float walkTimeStamp = 0f;
    public float gravity = 18;
    public GameObject myself;
    public Animator animator;
    //public GameObject heart;
    //public ParticleSystem fire;
    AudioSource audioSourceMeleeSound;

    public Rigidbody rb;

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    private void Start() {
        audioSourceMeleeSound = this.gameObject.GetComponent<AudioSource>();
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        float currentSpeed = (transform.position - lastPosition).magnitude;
        Animator.SetFloat("speed", currentSpeed * 10);
        lastPosition = transform.position;

        animator.SetBool("isShutDown", false);

        if (rb.velocity.y < -8) {
            this.enabled = false;
            animator.SetBool("isShutDown", true);
            animator.SetFloat("speed", 0);
            StartCoroutine(Destroy(5));
        }


        float distance = Vector3.Distance(target.position, transform.position);
        float level = Mathf.Abs((target.position - transform.position).y);
        if (distance <= viewRadius && level < 1) {
            FaceTarget(target);
            if (distance >= 1) {
                transform.position = Vector3.MoveTowards(transform.position, target.position,
                    Time.deltaTime * movementSpeed);
            }


            if (distance <= attackRadius && Time.time >= attackTimeStamp) {
                Animator.SetTrigger("scanTrigger");
                attackTimeStamp = Time.time + attackCoolDown;
                //TODO
                //target.GetComponent<Health>().DamagePlayer(25, this.gameObject);
                audioSourceMeleeSound.Play();
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

    IEnumerator Destroy(int interval) {
        //fire.gameObject.SetActive(true);
        yield return new WaitForSeconds(interval);
        //Instantiate(heart, new Vector3(0, .5f, 0) + transform.position, Quaternion.identity);
        Destroy(myself);
    }

    private void FixedUpdate() {
    }

    void FaceTarget(Transform currentTarget) {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
