
using UnityEngine;

public class TimeshiftActivator : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private GameObject[] _directObjects;
    [SerializeField] private GameObject[] _indirectObjects;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _borderWidth = 0.1f;
    [SerializeField] private Color _borderColor;
    
    private void Update()
    {
        if (_isActive)
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
                obj.GetComponent<TimeshiftRedirect>().Redirect(transform.position,_radius,_borderWidth,_borderColor);
            }
        }
    }
}
