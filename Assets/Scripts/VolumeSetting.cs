using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    //the UI Slider we use for changing the volume in our game.
    [SerializeField]
    Slider volumeSlider;

    //the Audio Mixer that handles our audio.
    [SerializeField]
    AudioMixer audioMixer;

    //the name of the audio parameter (exposed in the audio mixer) that we want to change.
    [SerializeField]
    string audioName = "MasterVolume";

    //used by the Slider when it's updated.
    public void ChangedVolume(float newVolume)
    {
        //the logarithm base 10, to determine the *actual* volume percentage
        float percentVolume = Mathf.Log10(newVolume);
       
        //Conver the percent (0-1) to db (-80 to 0)
        float totalVolume = Mathf.Lerp(-80f, 0, percentVolume);
        
        //Set the audio parameter that handles our volume.
        audioMixer.SetFloat(audioName, totalVolume);
    }

    //Load our saved volume before the game starts.
    private void Start()
    {
        LoadVolume();
    }

    //Save the current volume settings to the playerprefs.
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(audioName, volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        //should be 10 since thats equal to "0db" in the mixer.
        float volume = PlayerPrefs.GetFloat(audioName, 10f);
        ChangedVolume(volume);
        //reset the slider to the loaded volume.
        volumeSlider.value = volume;
    }


}
