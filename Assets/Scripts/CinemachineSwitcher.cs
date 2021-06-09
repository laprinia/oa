using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSwitcher : MonoBehaviour {
    public Animator ralphAnimator;
    public Animator astraAnimator;

    private Animator animator;

    private bool isAstraCamera = true;
    public bool canSwitch = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void SwitchState() {
        if (isAstraCamera) {
            animator.Play("Ralph camera");
            ralphAnimator.SetBool("ShutDown", false);
        } else {
            ralphAnimator.SetBool("ShutDown", true);
            animator.Play("Astra camera");
        }
        isAstraCamera = !isAstraCamera;
    }

    private void Update() {
        if (Input.GetKeyDown("space") && canSwitch) {
            SwitchState();
        }
    }
}
