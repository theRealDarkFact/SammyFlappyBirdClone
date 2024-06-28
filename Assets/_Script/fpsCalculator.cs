using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fpsCalculator : MonoBehaviour
{
    //FPS Calculator for Krappy Birds
    private TextMeshProUGUI fpsDisplayText;
    private float timer;
    private int fpsHighest = 0, fpsLowest = 1000;


    [SerializeField] private float RefreshRate = 1f;
    
    private void Start()
    {
        fpsDisplayText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsDisplayText.text = fps.ToString();
            timer = Time.unscaledTime + RefreshRate;
            if (fpsLowest != fps)
            {
                if (fpsLowest > fps && fps != 0) fpsLowest = fps;
            }
            if (fpsHighest != fps)
            {
                if (fpsHighest < fps) fpsHighest = fps;
            }

        }

    }
}
