using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CollectableInteract : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventoryObject;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private float _interactRadius = 5.0f;
    [SerializeField] private Transform _center;

    private void Update()
    {
        ShowNearestCollider();
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithCollectable();
        }
    }

    void InteractWithCollectable()
    {
        Collider nearest = GetNearestCollider();

        if (nearest != null && nearest.tag.Equals("Collectable"))
        {
            Debug.Log(nearest.name);
            Destroy(nearest.gameObject);
            
            if (nearest.GetComponent<Item>()!=null)
            {
                if (nearest.name.Equals("Neon Hex"))
                {
                    _inventoryObject.AddItem(nearest.GetComponent<Item>().itemObject, 1);
                }else
                {
                    _inventoryObject.AddItem(nearest.GetComponent<Item>().itemObject, 100);

                }
                
            }
            
        }
    }

    private void ShowNearestCollider()
    {
        Collider nearest = GetNearestCollider();
        if (nearest != null)
        {
            _canvas.SetActive(true);
            _textMeshPro.text = char.ToUpper(nearest.name[0]) + nearest.name.Substring(1).ToLower();
        }
        else _canvas.SetActive(false);
    }

    private Collider GetNearestCollider()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_center.position, _interactRadius);
        hitColliders = Array.FindAll(hitColliders, c => c.tag.Equals("Collectable"));
        Collider nearest = null;
        if (hitColliders.Length != 0)
        {
            nearest = hitColliders
                .OrderBy(collider => Vector3.Distance(_center.position, collider.transform.position)).ToArray()[0];
        }
        return nearest;
    }
}