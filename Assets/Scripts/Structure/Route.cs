using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Route
{
    public string name;
    public string text;
    public string spriteName;
    public int consequence;
    public List<int> completedEvents;
    public Dictionary<string, int> statsToAdd;
    public Dictionary<string, int> relationshipToAdd;

    public Route(string name, string text, int consequence, List<int> completedEvents, Dictionary<string, int> statsToAdd, Dictionary<string, int> relationshipToAdd)
    {
        this.name = name;
        this.text = text;
        this.consequence = consequence;
        this.completedEvents = completedEvents;
        this.statsToAdd = statsToAdd;
        this.relationshipToAdd = relationshipToAdd;
        this.spriteName = name;
    }

    public bool CheckCompletedEvents()
    {
        if(completedEvents == null || completedEvents.Count == 0) { return true; }
        else
        {
            foreach (int id in completedEvents)
                if (!Global.isEventDone(id))
                    return false;
            return true;
        }
    }

    public void GoThrough(string playerKey)
    {
        AddStats(playerKey);
        AddRelations(playerKey);
    }

    void AddStats(string playerKey)
    {
        if (statsToAdd == null || statsToAdd.Count == 0) { return; }
        Character character = Global.characters[playerKey];
        if (character.GetType() == typeof(Player))
            foreach (string key in statsToAdd.Keys)
            {
                (character as Player).AddStat(key, statsToAdd[key]);
            }
    }

    void AddRelations(string playerKey)
    {
        if (relationshipToAdd == null || relationshipToAdd.Count == 0) { return; }
        foreach (string key in relationshipToAdd.Keys)
        {
            Character character = Global.characters[key];
            if (character.GetType() == typeof(NPC))
                (character as NPC).AddRelationship(playerKey, relationshipToAdd[key]);
        }
    }
}
