using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeshiftRedirect :MonoBehaviour
{
    public void Redirect(Vector3 position, float radius, float borderWidth, Color borderColor)
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Renderer>().sharedMaterial.SetVector("_LightPosition",position);
            child.GetComponent<Renderer>().sharedMaterial.SetFloat("_LightRadius",radius);
            child.GetComponent<Renderer>().sharedMaterial.SetColor("_BorderColor",borderColor);
            child.GetComponent<Renderer>().sharedMaterial.SetFloat("_BorderWidth",borderWidth);
        }

    }
    
}
