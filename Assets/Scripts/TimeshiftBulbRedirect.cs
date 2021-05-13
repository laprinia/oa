using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeshiftBulbRedirect :MonoBehaviour
{
    public void Redirect(Vector3 position, float radius, float borderWidth, Color borderColor)
    {
        foreach(Transform c in transform)
        {
            c.transform.GetChild(2).GetComponent<Renderer>().sharedMaterial.SetVector("_LightPosition",position);
            c.transform.GetChild(2).GetComponent<Renderer>().sharedMaterial.SetFloat("_LightRadius",radius);
            c.transform.GetChild(2).GetComponent<Renderer>().sharedMaterial.SetColor("_BorderColor",borderColor);
            c.transform.GetChild(2).GetComponent<Renderer>().sharedMaterial.SetFloat("_BorderWidth",borderWidth);
        }

    }
    
}
