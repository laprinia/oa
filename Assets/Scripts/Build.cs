using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    public Material[] materialsForBuild;
    public Material borderBuildingMaterial;
    public GameObject building;
    public GameObject borderBuilding;

    public int flag = -1;
    public GameObject sphere;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider[] collisions;
            collisions = Physics.OverlapSphere(this.transform.position, 5);
            collisions = Array.FindAll(collisions, c => c.tag.Equals("Builder"));
            if ((collisions.Length != 0)) {
                if(flag < 12) {
                    if (flag == -1) {
                        borderBuilding.GetComponent<MeshRenderer>().material = borderBuildingMaterial;
                    } else {
                        Material[] mats = building.GetComponent<MeshRenderer>().materials;

                        mats[flag] = materialsForBuild[flag];


                        building.GetComponent<MeshRenderer>().materials = mats;
                    }
                    flag++;
                    if(flag == 12) {
                        sphere.SetActive(false);
                    }
                }
            }
        }
    }
}
