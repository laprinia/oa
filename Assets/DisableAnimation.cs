using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimation : MonoBehaviour
{ public Animator canvasAnimator;
   private void Update()
   {

      if (Input.GetKey(KeyCode.Space))
      {
         canvasAnimator.SetTrigger("disappear");
         Destroy(gameObject);
      }
      
   }
}
