using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    AudioMixer masterGroup;

    [SerializeField]
    string audioName = "MasterVolume";

    public void ChangedVolume(float newVolume)
    {
        float percentVolume = Mathf.Log10(newVolume);
       
        float totalVolume = Mathf.Lerp(-80f, 0, percentVolume);
        
        masterGroup.SetFloat(audioName, totalVolume);
    }

    private void Start()
    {
        LoadVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(audioName, volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat(audioName, 1f);
        ChangedVolume(volume);
        volumeSlider.value = volume;
    }


}
