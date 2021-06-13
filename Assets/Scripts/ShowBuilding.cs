using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuilding : MonoBehaviour
{
    public GameObject building;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "AstraTrigger") {
            building.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "AstraTrigger") {
            building.SetActive(false);
        }
    }
}
