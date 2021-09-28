using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotScreen : MonoBehaviour
{
    public float highSpaceBuffer;
    public float widthSpaceBuffer;
    public float textSpaceBuffer;
    public float size;
    public float topMergin;
    public RectTransform passedRoutePrefab;
    float currX;
    float currY;


    public void UpdateData()
    {
        Transform pointsObj = gameObject.transform.Find("Points");
        passedRoutePrefab.sizeDelta = new Vector2(size, size);
        currX = -widthSpaceBuffer / 2;
        float high = pointsObj.GetComponent<RectTransform>().sizeDelta.y;
        currY = -high / 2 + topMergin;

        for(int i = 0; i < pointsObj.childCount; i++)
        {
            Destroy(pointsObj.GetChild(i).gameObject);
        }

        Player currPlayer = Global.GetCharacter(Global.currPlayerKey) as Player;
        int j = 0;
        foreach (Route route in currPlayer.passedRoutes)
        {
            var newPassedRoute = Instantiate(passedRoutePrefab);
            newPassedRoute.SetParent(pointsObj);
            newPassedRoute.sizeDelta = new Vector2(size, size);
            newPassedRoute.localScale = new Vector2(1, 1);
            newPassedRoute.localPosition = new Vector2(currX, currY);
            newPassedRoute.Find("Sprite").GetComponent<Image>().sprite = Global.GetSprite(ResourceFolder.Routes, route.spriteName);
            newPassedRoute.Find("Name").GetComponent<Text>().text = route.name;
            newPassedRoute.Find("Name").localPosition = new Vector2(textSpaceBuffer, 0);
            j++;
            if (j % 2 == 0)
            {
                currX = -currX;
                textSpaceBuffer = -textSpaceBuffer;
            }
            currY += highSpaceBuffer;
        }
    }
}
