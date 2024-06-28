using UnityEngine;
using UnityEngine.Audio;


public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer MainMixer;
    //Must convert the linear scale of the slider to logrithmic scale of the audio mixer for the volumes. This must be done both way.


    public void SetMasterVolume(float sliderValue)
    {

        MainMixer.SetFloat("MainVolParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetBGMVolume(float sliderValue)
    {
        MainMixer.SetFloat("BGMVolParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        MainMixer.SetFloat("SFXVolParam", Mathf.Log10(sliderValue) * 20);
    }

}
