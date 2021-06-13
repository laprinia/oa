using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Animator canvasAnimator;
    public GameObject disableKey;
    public Material[] materialsForBuild;
    public Material borderBuildingMaterial;
    public GameObject building;
    public GameObject borderBuilding;

    public int flag = -1;
    public GameObject sphere;
    public ParticleSystem particleSystemBuild;

    private bool canContinue = true;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider[] collisions;
            collisions = Physics.OverlapSphere(this.transform.position, 5);
            collisions = Array.FindAll(collisions, c => c.tag.Equals("Builder"));
            if ((collisions.Length != 0)) {
                if(flag < 12 && canContinue) {
                    canContinue = false;
                    StartCoroutine(waitBeforeBuilding(1.5f));
                }
            }
        }
    }

    private IEnumerator waitBeforeBuilding(float waitTime) {
        particleSystemBuild.Play();
        yield return new WaitForSeconds(waitTime);
        if (flag == -1) {
            borderBuilding.GetComponent<MeshRenderer>().material = borderBuildingMaterial;
        } else {
            Material[] mats = building.GetComponent<MeshRenderer>().materials;

            mats[flag] = materialsForBuild[flag];


            building.GetComponent<MeshRenderer>().materials = mats;
        }
        flag++;
        if (flag == 12) {
            sphere.SetActive(false);
            canvasAnimator.SetTrigger("appear");
            disableKey.SetActive(true);
        }
        canContinue = true;
    }
}
