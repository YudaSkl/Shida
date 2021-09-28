using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    int index;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    void Start()
    {
        index = -2;
    }

    
    void Update()
    {
        Swipe();
    }

    void IndexChanged()
    {
        ChoiceAnimator leftChoice = GetComponent<Game>().leftChoice.GetComponent<ChoiceAnimator>();
        ChoiceAnimator rightChoice = GetComponent<Game>().rightChoice.GetComponent<ChoiceAnimator>();
        ChoiceAnimator bottomChoice = GetComponent<Game>().bottomChoice.GetComponent<ChoiceAnimator>();

        if (GetComponent<Game>().currPage.isChoicable())
        {
            switch (index)
            {
                case -1:
                    rightChoice.CloseAnimate();
                    bottomChoice.CloseAnimate();
                    leftChoice.CloseAnimate();
                    GetComponent<Game>().ShowHint("Choose");
                    break;
                case 0:
                    rightChoice.CloseAnimate();
                    bottomChoice.CloseAnimate();
                    if (GetComponent<Game>().leftChoice)
                        leftChoice.OpenAnimate();
                    break;
                case 1:
                    leftChoice.CloseAnimate();
                    bottomChoice.CloseAnimate();
                    if (GetComponent<Game>().rightChoice)
                        rightChoice.OpenAnimate();
                    break;
                case 2:
                    leftChoice.CloseAnimate();
                    rightChoice.CloseAnimate();
                    if (GetComponent<Game>().bottomChoice)
                        bottomChoice.OpenAnimate();
                    break;
                default: break;
            }
        }
        else
        {
            switch (index)
            {
                case -1:
                    GetComponent<Game>().ShowHint("Swipe") ;
                    break; 
                case 0:
                    GetComponent<Game>().Prev();
                    break;
                case 1:
                    GetComponent<Game>().Next();
                    break;
                default:
                    break;
            }
        }

    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = endTouchPosition - startTouchPosition;

            if(Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
            {
                //tap
                index = -1;
            }

            if (!stopTouch)
            {
                if (distance.x < -swipeRange)
                {
                    //left
                    index = 1;
                    stopTouch = true;
                }
                else if (distance.x > swipeRange)
                {
                    //right
                    index = 0;
                    stopTouch = true;
                }
                else if (distance.y < -swipeRange)
                {
                    //down
                    index = 3;
                    stopTouch = true;
                }
                else if (distance.y > swipeRange)
                {
                    //up
                    index = 2;
                    stopTouch = true;
                }
                IndexChanged();
            }
        }
    }
}
