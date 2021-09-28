using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsButton : MonoBehaviour
{
    public Sprite hintsOn;
    public Sprite hintsOff;
    Image image;
    bool hintsEnabled;

    void Start()
    {
        hintsEnabled = true;
        image = GetComponent<Image>();
    }

    void SetHints(bool enabled)
    {
        if (enabled)
        {
            Settings.SetHints(true);
            image.sprite = hintsOn;
        }
        else
        {
            Settings.SetHints(false);
            image.sprite = hintsOff;
        }
    }

    public void SwitchHints()
    {
        hintsEnabled = !hintsEnabled;
        SetHints(hintsEnabled);
    }
}
