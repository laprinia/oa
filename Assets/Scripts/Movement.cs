using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed = 2.5f;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] private float _aimSmoothStep = 0.3f;
    Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();


    void Update()
    {
        AimTowardsMousePosition();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        _animator.SetFloat("zVelocity", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("xVelocity", velocityX, 0.1f, Time.deltaTime);
    }

    void AimTowardsMousePosition()
    {
        if (!(Camera.main is null))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
            {
                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.forward = Vector3.Lerp(transform.forward,direction,0.3f);
            }
        }
    }
}