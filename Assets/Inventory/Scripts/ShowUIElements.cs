using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIElements : MonoBehaviour
{
    private bool isInventoryShowing;
    [SerializeField] private Animator _animator;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryShowing = !isInventoryShowing;
            _animator.ResetTrigger(isInventoryShowing?"disappear":"appear");
            _animator.SetTrigger(isInventoryShowing?"appear":"disappear");
            
        }
    }
    
    
}
