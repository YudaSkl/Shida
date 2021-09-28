using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarButton : MonoBehaviour
{
    public GameObject panel;
    public Text nameText;
    public Text statusText;

    public void AvatarTapped()
    {
        panel.SetActive(true);
        if (gameObject.GetComponent<CharacterAvatar>().character.GetType() == typeof(Player))
        {
            string char_name = gameObject.GetComponent<CharacterAvatar>().character.char_name;
            string char_status = gameObject.GetComponent<CharacterAvatar>().character.GetStatus();
            nameText.text = char_name;
            statusText.text = char_status + "it is player";
        }
        else
        {
            string char_name = gameObject.GetComponent<CharacterAvatar>().character.char_name;
            string char_status = gameObject.GetComponent<CharacterAvatar>().character.GetStatus();
            nameText.text = char_name;
            statusText.text = char_status;
        }
    }
}
