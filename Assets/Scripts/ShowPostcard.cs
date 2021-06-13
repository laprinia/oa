using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPostcard : MonoBehaviour
{
   [SerializeField] private Animator _animator;
   public void ShowPost()
   {
      _animator.SetTrigger("appear");
   }
   public void HidePost()
   {
      _animator.SetTrigger("diasppear");
   }
}
