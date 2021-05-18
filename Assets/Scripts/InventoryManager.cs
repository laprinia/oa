using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventoryObject;
    [SerializeField] private GameObject katana;
    private void Awake()
    {
        _inventoryObject.AddItem(katana.GetComponent<Item>().itemObject, 1);
    }

}
