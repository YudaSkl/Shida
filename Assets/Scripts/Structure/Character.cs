using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string avatarName;
    public string char_name;
    public string key;
    protected string status;


    public Character(string char_name, string key, string avatarName, Status status) {
        this.char_name = char_name;
        this.key = key;
        this.avatarName = avatarName;
        SetStatus(status);
    }

    public string GetStatus()
    {
        return status;
    }

    public void SetStatus(Status status)
    {
        this.status = Enum.GetName(typeof(Status), status);
    }
}
