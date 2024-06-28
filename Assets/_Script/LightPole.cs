using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPole : MonoBehaviour
{
    [SerializeField] private Light2D LightPoleLight;
    private NightDay ndCycler;
    private void Start()
    {
        ndCycler = FindObjectOfType<NightDay>();
    }

    private void Update()
    {
        CheckLight();    
    }

    private void CheckLight() 
    {
        if (ndCycler.dayState() && !LightPoleLight.enabled)
        {
            LightPoleLight.enabled = true;
        }
        else if (!ndCycler.dayState() && LightPoleLight.enabled)
        {
            LightPoleLight.enabled = false;
        }
    }
}
