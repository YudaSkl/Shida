using System;
using System.Collections.Generic;


[Serializable]
public class Story
{
    public List<Event> events;
    public int currEventID;
    public int currPageID;

    public Story()
    {
        currEventID = 0;
        currPageID = 0;
        events = new List<Event>();
    }
    public void AddEvents(List<Event> events)
    {
        this.events = events;
    }
}
