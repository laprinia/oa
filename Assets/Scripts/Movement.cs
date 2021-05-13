using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed = 2.5f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    Animator _animator;
    private float turnSmooth;

    private void Awake() => _animator = GetComponent<Animator>();


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float runSpeed = 1;
        Vector3 movement = new Vector3(horizontal * runSpeed, 0.0f, vertical * runSpeed);
        _animator.SetFloat("zVelocity", Input.GetAxisRaw("Vertical") * runSpeed, 0.3f, Time.deltaTime);
        _animator.SetFloat("xVelocity", Input.GetAxisRaw("Horizontal") * runSpeed, 0.3f, Time.deltaTime);
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            float cameraAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Camera.main.transform.localEulerAngles.y,
                ref turnSmooth, _turnSmoothTime);
            transform.localEulerAngles =
                new Vector3(transform.localEulerAngles.x, cameraAngle, transform.localEulerAngles.z);

            transform.Translate(movement, Space.Self);
        }
    }
}