using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public bool ralphTimeToLiveExpired;
    public float timeToWait;
    
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
