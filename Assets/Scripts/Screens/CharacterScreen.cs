using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreen : MonoBehaviour
{
    Dictionary<string, int> stats;
    public List<Slider> sliders;
    public Transform charInfo;

    public void UpdateData()
    {
        Player p = Global.characters["Player"] as Player;
        SetCharInfo(p);
        stats = p.stats;
        int i = 0;
        foreach (int val in stats.Values)
        {
            sliders[i].value = val;
            i++;
        }
    }

    public void SetCharInfo(Player p)
    {
        Image img = charInfo.GetChild(1).GetComponent<Image>();
        Sprite avatar = Global.GetSprite(ResourceFolder.Avatars, p.avatarName);
        img.sprite = avatar;

        charInfo.GetChild(2).GetComponent<Text>().text = p.char_name;
        charInfo.GetChild(3).GetComponent<Text>().text = p.GetStatus();
    }
}
