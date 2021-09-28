using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Sprite volumeOn;
    public Sprite volumeOff;
    Image image;
    bool volEnabled;

    void Start()
    {
        volEnabled = true;
        image = GetComponent<Image>();
    }

    void SetAudio(bool enabled)
    {
        if (enabled)
        {
            Settings.SetVolume(true);
            image.sprite = volumeOn;
        }
        else
        {
            Settings.SetVolume(false);
            image.sprite = volumeOff;
        }
    }

    public void SwitchAudio()
    {
        volEnabled = !volEnabled;
        SetAudio(volEnabled);
    }
}