using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventoryObject;
   // [SerializeField] private GameObject addeable;
    private void Awake()
    {
       // _inventoryObject.AddItem(addeable.GetComponent<Item>().itemObject, 1);
    }

}
