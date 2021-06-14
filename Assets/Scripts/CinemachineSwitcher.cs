using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour
{
    public float audioFadeTime;
    public AudioSource ralphAudio;
    public AudioSource astraAudio;
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
        if (isAstraCamera)
        {
            StartCoroutine(StartFade(astraAudio, audioFadeTime, 0));
            StartCoroutine(StartFade(ralphAudio, audioFadeTime, 0.1f));
            
            isAstraCamera = !isAstraCamera;
            StartCoroutine(WaitBeforeMovingRalph(1.0f));
            isLerping = true;
            canSwitch = true;
            ralphHUD.SetActive(true);
            timeToLive.ResetTimer();
        } else
        {
            StartCoroutine(StartFade(ralphAudio, audioFadeTime, 0));
            StartCoroutine(StartFade(astraAudio, audioFadeTime, 0.1f));
            
            isLerping = true;
            isAstraCamera = !isAstraCamera;
            ralphAnimator.SetBool("ShutDown", true);
            animator.Play("Astra camera");
            ralph.GetComponent<Movement>().enabled = false;
            StartCoroutine(WaitBeforeMoving(3.2f, astra));
            StartCoroutine(WaitBeforeSwitching(60));
            ralphHUD.SetActive(false);
        }
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
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
            if ((collisions.Length != 0 && canSwitch) || (!isAstraCamera && timeToLive.ralphTimeToLiveExpired)) {
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

    private IEnumerator WaitBeforeMovingRalph(float waitTime) {
        astra.GetComponent<Movement>().enabled = false;
        astraAnimator.SetBool("Construction", true);
        yield return new WaitForSeconds(4.0f);
        ralphAnimator.SetBool("ShutDown", false);
        astraAnimator.SetBool("Construction", false);
        animator.Play("Ralph camera");
        yield return new WaitForSeconds(waitTime);
        ralph.GetComponent<Movement>().enabled = true;
    }

    private IEnumerator WaitBeforeSwitching(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        canSwitch = true;
    }
}
