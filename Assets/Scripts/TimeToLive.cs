using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeToLive : MonoBehaviour
{
    public bool ralphTimeToLiveExpired;
    public float timeToWait;
    public Image ringBar;
    public Color startColor, endColor;
    private float lerpSpeed;
    private float totalTimeToWait;
    

    private void Start() => totalTimeToWait = timeToWait;

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        ColorChanger();
        HealthBarFiller();
    }
    void HealthBarFiller()
    {
       
        ringBar.fillAmount = Mathf.Lerp(ringBar.fillAmount, (timeToWait / totalTimeToWait), lerpSpeed);

    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(endColor, startColor, (timeToWait / totalTimeToWait));
        ringBar.color = healthColor;
    }

    public void ResetTimer()
    {
        ralphTimeToLiveExpired = false;
        StartCoroutine(TimeCoroutine());
    }

    IEnumerator TimeCoroutine()
    {
        while (timeToWait > 0)
        {
            yield return new WaitForSeconds(1f);
            timeToWait--;
        }
        ralphTimeToLiveExpired = true;
        Debug.Log("Time expired");
    } 
}
