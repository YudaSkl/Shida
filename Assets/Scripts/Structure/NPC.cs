using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : Character
{
    public bool isMeet;
    Dictionary<string, int> relationships;
    short maxRel = 2;
    short minRel = -2;

    public NPC(string char_name, string key, Status status, string avatarName) : base (char_name, key, avatarName, status)
    {
        relationships = new Dictionary<string, int>();
        isMeet = false;
    }

    public void AddRelationship(string playerKey, int valueToAdd)
    {
        if (!relationships.ContainsKey(playerKey))
        {
            relationships.Add(playerKey, 0);
        }

        relationships[playerKey] += valueToAdd;

        if (relationships[playerKey] > maxRel)
        {
            relationships[playerKey] = maxRel;
        }
        else if (relationships[playerKey] < minRel)
        {
            relationships[playerKey] = minRel;
        }
        RelationshipChanged(playerKey);
    }

    public void RelationshipChanged(string playerKey)
    {
        switch (relationships[playerKey])
        {
            case -2: SetStatus(Status.enemy); break;
            case -1: SetStatus(Status.rejection); break;
            case 0: SetStatus(Status.netural); break;
            case 1: SetStatus(Status.sympathy); break;
            case 2: SetStatus(Status.friend); break;
            default: SetStatus(Status.none);  break;
        }
    }
}
