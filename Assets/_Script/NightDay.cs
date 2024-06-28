using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightDay : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float dayCycleSpeed = 2;
    [SerializeField] private float dayCycleCounter = 1;
    [SerializeField] private bool DayNight = false;

    [SerializeField] private bool isDay = true;
    [SerializeField] private float DayCycleStartValue;

    private void Start()
    {
        DayCycleStartValue = dayCycleCounter;
    }

    private void TimeOfDay()
    {
        globalLight.intensity = Mathf.Clamp(Mathf.Sin(dayCycleSpeed * dayCycleCounter), 0.25f, 1f);
        if (globalLight.intensity < 0.58f)
        {
            globalLight.color = Color.Lerp(globalLight.color, Color.blue, 0.1f * Time.deltaTime);
            isDay = true;

        }
        else if (globalLight.intensity > 0.58f && globalLight.intensity < 0.78f)
        {
            globalLight.color = Color.Lerp(globalLight.color, Color.red, 0.1f * Time.deltaTime);

        }
        else
        {
            globalLight.color = Color.Lerp(globalLight.color, Color.white, 0.1f * Time.deltaTime);
            isDay = false;
        }
    }

    public void ToggleDayNight() 
    {
        DayNight = !DayNight;
    }

    public bool dayState()
    {
        return isDay;
    }

    private void Update()
    {
        if (DayNight)
        {
            dayCycleCounter += Time.deltaTime;
            if (dayCycleCounter >= 80) dayCycleCounter = 0;
            TimeOfDay();
        }
    }
}
