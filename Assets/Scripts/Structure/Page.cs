using System;
using System.Collections.Generic;

[Serializable]
public class Page
{
    private string text;
    private string author_id;
    public bool choiceable;

    public Page(string text, string author_id)
    {
        this.author_id = author_id;
        this.text = text;
        choiceable = false;
    }

    public Character GetAuthor()
    {
        return Global.characters[author_id];
    }

    public string GetText()
    {
        return text;
    }

    public bool isChoicable()
    {
        return choiceable;
    }
}