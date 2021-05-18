using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIElements : MonoBehaviour
{
    private bool isInventoryShowing;
    [SerializeField]private GameObject inventoryCanvas;


    private void Start()
    {
        inventoryCanvas.SetActive(false);
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryShowing = !isInventoryShowing;
            inventoryCanvas.SetActive(isInventoryShowing);

        }
    }
}
