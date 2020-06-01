using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    public void UpdateCameraInversion(bool invert)
    {
        //send this to the player to fix rotation on X axis (up and down)
        //PlayerPrefs. Save this
        Debug.Log(invert);
        player.InvertXRotation = invert;
    }

    private void OnDisable()
    {
        player.enabled = true;
    }
}
