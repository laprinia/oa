using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]private GameObject interactUI;
    [SerializeField]private GameObject HUDUI;
    [SerializeField]private GameObject inventoryUI;
    private void Start()
    {
        interactUI.SetActive(false);
        HUDUI.SetActive(false);
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}
