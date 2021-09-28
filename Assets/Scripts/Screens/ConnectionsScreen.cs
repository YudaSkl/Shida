using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionsScreen : MonoBehaviour
{
    public GameObject infoPanel;
    Text nameTexbox;
    Text statusTexbox;
    public ScrollRect view;
    public RectTransform characterAvatarPrefab;
    public RectTransform linePrefab;

    List<GameObject> npcs;
    List<Vector2> points;
    public int distanceBuffer;
    public int size;


    public void UpdateData()
    {
        GeneratePoints();
        if (npcs == null)
        {
            npcs = new List<GameObject>();
            characterAvatarPrefab.sizeDelta = new Vector2(size, size);
            nameTexbox = infoPanel.transform.Find("Name").GetComponent<Text>();
            statusTexbox = infoPanel.transform.Find("Status").GetComponent<Text>();

            var player = Instantiate(characterAvatarPrefab);
            player.transform.SetParent(view.transform, false);
            player.GetComponent<CharacterAvatar>().character = Global.characters[Global.GetCharacterKey(CharacterKey.Player)];
            player.GetComponent<CharacterAvatar>().SetPosition(new Vector2(0, 0));
            player.Find("avatar").GetComponent<Image>().sprite = Global.GetSprite(ResourceFolder.Avatars, player.GetComponent<CharacterAvatar>().character.avatarName);
            player.GetComponent<AvatarButton>().panel = infoPanel;
            player.GetComponent<AvatarButton>().nameText = nameTexbox;
            player.GetComponent<AvatarButton>().statusText = statusTexbox;
            GeneratePoints();
        }
        SpawnSocial();
    }

    //Not realized
    public void DrawLine(Vector2 p1, Vector2 p2)
    {
        var newLine = Instantiate(linePrefab);
        float high = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(p1.x - p2.x), 2) + Mathf.Pow(Mathf.Abs(p1.y - p2.y), 2));
        float width = high / 4;
        newLine.transform.SetParent(view.transform, false);
        newLine.sizeDelta = new Vector2(width, high);
        newLine.position = new Vector2(width/2, high/2);
    }

    public void GeneratePoints()
    {
        if (points != null && points.Count != 0)
            return;
        points = new List<Vector2>();
        points.Add(new Vector2(0, distanceBuffer));
        points.Add(new Vector2(distanceBuffer, -distanceBuffer / 2));
        points.Add(new Vector2(-distanceBuffer, -distanceBuffer / 2));

        points.Add(new Vector2(distanceBuffer, distanceBuffer/2));
        points.Add(new Vector2(0, -distanceBuffer));
        points.Add(new Vector2(-distanceBuffer, distanceBuffer / 2));

        points.Add(new Vector2(distanceBuffer, distanceBuffer + distanceBuffer / 3));
        points.Add(new Vector2(2*distanceBuffer, 0));
        points.Add(new Vector2(distanceBuffer, - distanceBuffer - distanceBuffer / 3));

        points.Add(new Vector2(- distanceBuffer, - distanceBuffer - distanceBuffer / 3));
        points.Add(new Vector2(- 2 * distanceBuffer, 0));
        points.Add(new Vector2(-distanceBuffer, distanceBuffer + distanceBuffer / 3));
    }

    public void SpawnSocial()
    {
        Transform spawnObj = gameObject.transform.Find("Scroll").Find("Spawn");

        for (int i = 0; i < spawnObj.childCount; i++)
        {
            Destroy(spawnObj.GetChild(i).gameObject);
        }
        npcs = new List<GameObject>();
        foreach (string key in Global.characters.Keys)
        {
            Character character = Global.characters[key];
            if (character.GetType() == typeof(NPC) && (character as NPC).isMeet)
            {
                var newChar = (Transform)Instantiate(characterAvatarPrefab);
                newChar.transform.SetParent(view.transform, false);
                newChar.GetComponent<CharacterAvatar>().character = character;
                newChar.GetComponent<AvatarButton>().panel = infoPanel;
                newChar.GetComponent<AvatarButton>().nameText = nameTexbox;
                newChar.GetComponent<AvatarButton>().statusText = statusTexbox;
                newChar.transform.Find("avatar").GetComponent<Image>().sprite = Global.GetSprite(ResourceFolder.Avatars, newChar.GetComponent<CharacterAvatar>().character.avatarName);
                npcs.Add(newChar.gameObject);

                newChar.GetComponent<CharacterAvatar>().SetPosition(points[npcs.Count - 1]);                
            }
        }

    }

    public void ViewTapped()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
        }
    }
}
