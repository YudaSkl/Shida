using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Image avatar;
    Text char_name;

    public Text pageText;
    public Transform charInfo;
    public Page currPage;

    Text status;
    Event currEvent;
    Player currPlayer;

    public GameObject leftChoice;
    public GameObject rightChoice;
    public GameObject bottomChoice;

    Text leftChoiceText;
    Text rightChoiceText;
    Text bottomChoiceText;

    Route leftRoute;
    Route rightRoute;
    Route bottomRoute;

    public float hintWaitTime;
    public GameObject hints;
    GameObject swipeHint;
    GameObject chooseHint;
    bool isHintShown;
    List<GameObject> hintArrowsToShow;
    float hintTimer = 0;
    string hintMode;

    private void Start()
    {
        isHintShown = true;
        hintArrowsToShow = new List<GameObject>();
        hintMode = "none";
        swipeHint = hints.transform.Find("Swipe").gameObject;
        chooseHint = hints.transform.Find("Choose").gameObject;

        leftChoiceText = leftChoice.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        rightChoiceText = rightChoice.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        bottomChoiceText = bottomChoice.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        if(Global.story == null)
        {
            Global.NewGame();
        }
        currEvent = Global.story.events[Global.story.currEventID];
        currPage = currEvent.pages[Global.story.currPageID];
        GetPlayer();

        avatar = charInfo.GetChild(1).GetComponent<Image>();
        char_name = charInfo.GetChild(2).GetComponent<Text>();
        status = charInfo.GetChild(3).GetComponent<Text>();
        ShowPage();
    }

    public void GetPlayer()
    {
        currPlayer = Global.GetCharacter(Global.currPlayerKey) as Player;
    }

    public void Update()
    {
        if (!isHintShown)
        {
            if (hintTimer > hintWaitTime)
            {
                isHintShown = true;
                switch (hintMode)
                {
                    case "Swipe":
                        swipeHint.SetActive(true);
                        chooseHint.SetActive(false);
                        break;
                    case "Choose":
                        swipeHint.SetActive(false);
                        chooseHint.SetActive(true);
                        break;
                    default:
                        break;
                }
                foreach (GameObject arrow in hintArrowsToShow)
                {
                    arrow.SetActive(true);
                }
            }
            hintTimer += Time.deltaTime;
        }
    }

    public void ShowHint(string hintMode)
    {
        foreach (GameObject arrow in hintArrowsToShow)
        {
            arrow.SetActive(false);
        }
        hintArrowsToShow = new List<GameObject>();
        swipeHint.SetActive(false);
        chooseHint.SetActive(false);
        switch (hintMode)
        {
            case "Swipe":
                if (Global.story.currPageID == 0)
                {
                    hintArrowsToShow.Add(swipeHint.transform.Find("RightArrow").gameObject);
                }
                else if (Global.story.currPageID < currEvent.pages.Count)
                {
                    hintArrowsToShow.Add(swipeHint.transform.Find("RightArrow").gameObject);
                    hintArrowsToShow.Add(swipeHint.transform.Find("LeftArrow").gameObject);
                }
                break;
            case "Choose":
                if (currEvent.routes.Count == 1)
                {
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromBottomArrow").gameObject);
                }
                else if (currEvent.routes.Count == 2)
                {
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromRightArrow").gameObject);
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromLeftArrow").gameObject);
                }
                else if (currEvent.routes.Count == 3)
                {
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromRightArrow").gameObject);
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromLeftArrow").gameObject);
                    hintArrowsToShow.Add(chooseHint.transform.Find("FromBottomArrow").gameObject);
                }
                break;
            default:
                break;
        }
        isHintShown = false;
        hintTimer = 0;
        this.hintMode = hintMode;
    }

    void ShowPage()
    {
        currPage = currEvent.pages[Global.story.currPageID];
        pageText.text = currPage.GetText();

        avatar.sprite = Global.GetSprite(ResourceFolder.Avatars, currPage.GetAuthor().avatarName);
        char_name.text = currPage.GetAuthor().char_name;
        status.text = currPage.GetAuthor().GetStatus();
        
        if (currPage.GetAuthor().GetType() == typeof(NPC))
        {
            (currPage.GetAuthor() as NPC).isMeet = true;
        }

        if (!currPage.isChoicable())
        {
            leftChoice.SetActive(false);
            rightChoice.SetActive(false);
            bottomChoice.SetActive(false);
            if (Settings.isHintsEnable)
                ShowHint("Swipe");
        }
        else
        {
            if(Settings.isHintsEnable)
                ShowHint("Choose");
            List<Route> routes = currEvent.routes;
            if (routes != null)
            {
                int i = 0;
                if (routes.Count == 1)
                {
                    foreach (Route route in routes)
                    {
                        bottomChoice.SetActive(true);
                        bottomChoiceText.text = route.text;
                        bottomRoute = route;
                        break;
                    }
                }
                else { 
                    foreach (Route route in routes)
                    {
                        i++;
                        if (i == 1)
                        {
                            leftChoice.SetActive(true);
                            leftChoiceText.text = route.text;
                            leftRoute = route;
                        }
                        else if (i == 2)
                        {
                            rightChoice.SetActive(true);
                            rightChoiceText.text = route.text;
                            rightRoute = route;
                        }
                        else if (i == 3)
                        {
                            bottomChoice.SetActive(true);
                            bottomChoiceText.text = route.text;
                            bottomRoute = route;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    public void Prev()
    {
        if (Global.story.currPageID > 0)
        {
            Global.story.currPageID -= 1;
            Global.SaveStory();
            ShowPage();
        }
    }

    public void Next()
    {
        if (Global.story.currPageID < currEvent.pages.Count - 1)
        {
            Global.story.currPageID += 1;
            Global.SaveStory();
            ShowPage();
        }
    }

    public void Left()
    {
        GoToNextEvent(leftRoute); 
    }
    
    public void Right()
    {
        GoToNextEvent(rightRoute);
    }

    public void Bottom()
    {
        GoToNextEvent(bottomRoute);
    }

    public void GoToNextEvent(Route choosedRoute)
    {
        currEvent.EndEvent();
        Global.story.currPageID = 0;
        Global.story.currEventID = choosedRoute.consequence;
        currEvent = Global.story.events[choosedRoute.consequence];
        currPlayer.GoThroughRoute(choosedRoute);
        Global.SaveAll();
        ShowPage();
    }
}
