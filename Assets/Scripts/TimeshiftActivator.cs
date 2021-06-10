
using System;
using System.Linq;
using UnityEngine;

public class TimeshiftActivator : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private GameObject[] _directObjects;
    [SerializeField] private GameObject[] _indirectObjects;
    [SerializeField] private float _radius = 0.001f;
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
            else if(obj.GetComponent<TimeshiftBulbRedirect>() !=null)
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
        var bulbPlantParentGo = Array.FindAll(_indirectObjects, o => o.GetComponent<TimeshiftBulbRedirect>() != null)[0];
        var collectableParentGo=Array.FindAll(_indirectObjects, o => o.tag.Equals("Collectable"))[0];
        foreach (Transform bulb in bulbPlantParentGo.transform)
        {

            if (PointInsideRadius(bulb.position))
            {
                bulb.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                bulb.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                bulb.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                bulb.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            }

        }

        foreach (Transform collect in collectableParentGo.transform)
        {
            if (PointInsideRadius(collect.position))
            {
                collect.gameObject.SetActive(true);
            }
            else
            {
                collect.gameObject.SetActive(false);
            }
        }
        

        

    }
}

