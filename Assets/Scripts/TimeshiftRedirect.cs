using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeshiftRedirect :MonoBehaviour
{
    public void Redirect(Vector3 position, float radius, float borderWidth, Color borderColor)
    {
        foreach(Transform c in transform)
        {
            c.GetComponent<Renderer>().sharedMaterial.SetVector("_LightPosition",position);
            c.GetComponent<Renderer>().sharedMaterial.SetFloat("_LightRadius",radius);
            c.GetComponent<Renderer>().sharedMaterial.SetColor("_BorderColor",borderColor);
            c.GetComponent<Renderer>().sharedMaterial.SetFloat("_BorderWidth",borderWidth);
        }

    }
    
}
