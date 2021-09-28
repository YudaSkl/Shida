using System;
using UnityEngine;
using UnityEngine.UI;
public class CharacterAvatar : MonoBehaviour
{
    public Character character;

    public CharacterAvatar()
    {
    }

    public Vector2 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void SetPosition(Vector2 position)
    {
        gameObject.transform.localPosition = position;
    }
}
