using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour {
    public Animator ralphAnimator;
    public Animator astraAnimator;
    public GameObject ralphHUD;
    public TimeToLive timeToLive;
    private Animator animator;

    private bool isAstraCamera = true;
    public bool canSwitch = true;
    public bool isLerping = false;

    private float initialValueRalphZone = 0;
    private float valueRalphRedZoneToChange = -1;
    private float t = 0.0f;

    public GameObject ralph;
    public GameObject astra;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void SwitchState() {
        if (isAstraCamera) {
            isAstraCamera = !isAstraCamera;
            ralphAnimator.SetBool("ShutDown", false);
            animator.Play("Ralph camera");
            astra.GetComponent<Movement>().enabled = false;
            StartCoroutine(WaitBeforeMoving(1.0f, ralph));
            isLerping = true;
            canSwitch = true;
            ralphHUD.SetActive(true);
            timeToLive.ResetTimer();
        } else {
            isLerping = true;
            isAstraCamera = !isAstraCamera;
            ralphAnimator.SetBool("ShutDown", true);
            animator.Play("Astra camera");
            ralph.GetComponent<Movement>().enabled = false;
            StartCoroutine(WaitBeforeMoving(3.2f, astra));
            canSwitch = true;
            ralphHUD.SetActive(false);
        }
    }

    private void Update() {
        if (Input.GetKeyDown("space") ||(!isAstraCamera && timeToLive.ralphTimeToLiveExpired)) {
            Collider[] collisions;
            if (isAstraCamera) {
                collisions = Physics.OverlapSphere(astra.transform.position, 5);
                collisions = Array.FindAll(collisions, c => c.tag.Equals("RalphTrigger"));
            } else {
                collisions = Physics.OverlapSphere(ralph.transform.position, 5);
                collisions = Array.FindAll(collisions, c => c.tag.Equals("AstraTrigger"));
            }
            if (collisions.Length != 0 && canSwitch) {
                Debug.Log("aici");
                canSwitch = false;
                SwitchState();
                initialValueRalphZone = ralph.GetComponent<TimeshiftActivator>().GetRadius();
                if (initialValueRalphZone <= 4.5f) {
                    valueRalphRedZoneToChange = 5;
                } else {
                    valueRalphRedZoneToChange = 0.000001f;
                }
                t = 0.0f;
            } else {
                Debug.Log("not in range to switch");
            }
        }

        if (isLerping) {
            ralph.GetComponent<TimeshiftActivator>().SetRadius(Mathf.Lerp(initialValueRalphZone, valueRalphRedZoneToChange, t));
            
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f) {
                isLerping = false;
                ralph.GetComponent<TimeshiftActivator>().SetRadius(valueRalphRedZoneToChange);
            }
        }
    }

    private IEnumerator WaitBeforeMoving(float waitTime, GameObject character) {
        yield return new WaitForSeconds(waitTime);
        character.GetComponent<Movement>().enabled = true;
    }
}
