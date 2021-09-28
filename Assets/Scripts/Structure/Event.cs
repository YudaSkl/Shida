using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Event
{
    public int id;
    public bool done;
    public string name;
    public List<Page> pages;
    public List<Route> routes;


    public Event(int id, string name, List<Page> pages, List<Route> routes)
    {
        this.id = id;
        done = false;
        this.name = name;
        this.routes = routes;
        pages[pages.Count - 1].choiceable = true;
        this.pages = pages;
    }

    public void EndEvent()
    {
        done = true;
    }
}