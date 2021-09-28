using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : Character
{
    public Dictionary<string, int> stats;
    public List<Route> passedRoutes;

    public Player(string name, string key, Status status, string avatarName) : base(name, key, avatarName, status)
    {
        passedRoutes = new List<Route>();
        SetStats();
    }

    public void SetStats()
    {
        stats = new Dictionary<string, int>();
        foreach (string stat in Enum.GetNames(typeof(Stat)))
            stats.Add(stat, 0);
    }

    public void AddStat(string stat, int add_value)
    {
        stats[stat] += add_value;
    }

    public void GoThroughRoute(Route route)
    {
        route.GoThrough(key);
        passedRoutes.Add(route);
    }
}
