using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour {
    public Animator ralphAnimator;
    public Animator astraAnimator;

    private Animator animator;

    private bool isAstraCamera = true;
    public bool canSwitch = false;

    public GameObject ralph;
    public GameObject astra;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void SwitchState() {
        if (isAstraCamera) {
            animator.Play("Ralph camera");
            ralphAnimator.SetBool("ShutDown", false);
            astra.GetComponent<Movement>().enabled = false;
            StartCoroutine(WaitBeforeMoving(2.5f, ralph));
        } else {
            ralphAnimator.SetBool("ShutDown", true);
            animator.Play("Astra camera");
            ralph.GetComponent<Movement>().enabled = false;
            StartCoroutine(WaitBeforeMoving(2.5f, astra));
        }
        isAstraCamera = !isAstraCamera;
    }

    private void Update() {
        if (Input.GetKeyDown("space") && canSwitch) {
            SwitchState();
        }
    }

    private IEnumerator WaitBeforeMoving(float waitTime, GameObject character) {
        yield return new WaitForSeconds(waitTime);
        character.GetComponent<Movement>().enabled = true;
    }
}
