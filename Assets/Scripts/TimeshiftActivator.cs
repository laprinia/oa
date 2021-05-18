
using System;
using System.Linq;
using UnityEngine;

public class TimeshiftActivator : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private GameObject[] _directObjects;
    [SerializeField] private GameObject[] _indirectObjects;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _borderWidth = 0.1f;
    [SerializeField] private Color _borderColor;
    [SerializeField] private Transform _center;
    
    private void Update()
    {
        if (_isActive)
        {
            ActivateObjects();
            ShowObjects();
        }
    }

    private void ActivateObjects()
    {
        foreach (var obj in _directObjects)
        { 
            obj.GetComponent<Renderer>().sharedMaterial.SetVector("_LightPosition",transform.position);
            obj.GetComponent<Renderer>().sharedMaterial.SetFloat("_LightRadius", _radius);
            obj.GetComponent<Renderer>().sharedMaterial.SetColor("_BorderColor",_borderColor);
            obj.GetComponent<Renderer>().sharedMaterial.SetFloat("_BorderWidth",_borderWidth);
        }

        foreach (var obj in _indirectObjects)
        {
            if (obj.GetComponent<TimeshiftRedirect>() != null)
            {
                obj.GetComponent<TimeshiftRedirect>().Redirect(transform.position,_radius,_borderWidth,_borderColor);
            }
            else
            {
                obj.GetComponent<TimeshiftBulbRedirect>().Redirect(transform.position,_radius,_borderWidth,_borderColor);
            }
        }

    }

    private bool PointInsideRadius(Vector3 position)
    {
        return Vector3.Distance(position, _center.position) < _radius-0.5f;
    }
    private void ShowObjects()
    {
        var go = Array.FindAll(_indirectObjects, o => o.GetComponent<TimeshiftBulbRedirect>() != null)[0];
       
        foreach (Transform c in go.transform)
        {

            if (PointInsideRadius(c.position))
            {
                c.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                c.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                c.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                c.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            }

        }

        

    }
}

