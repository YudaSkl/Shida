using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceAnimator: MonoBehaviour
{
    public bool isOpened;
    
    void Start()
    {
        isOpened = false;
    }

    void Update()
    {
        
    }
    public void OpenAnimate()
    {
        if (!isOpened) {
            Animation anim = GetComponent<Animation>();
            if (!anim.isPlaying)
            {
                isOpened = true;
                anim.Play(gameObject.name + "OpenAnimation");
            }
        }
    }
    public void CloseAnimate()
    {
        if (isOpened)
        {
            Animation anim = GetComponent<Animation>();
            if (!anim.isPlaying)
            {
                isOpened = false;
                anim.Play(gameObject.name + "CloseAnimation");
            }
        }
    }
}
