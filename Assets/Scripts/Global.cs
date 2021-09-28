using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Global
{
    static public Dictionary<string,Character> characters;
    static public Story story;
    static public string currPlayerKey;
    static public string charactersFilename = "/characters.yuda";
    static public string storyFilename = "/story.yuda";
    static public string settingsFilename = "/settings.yuda";

    static public void NewGame()
    {
        Settings.SetDefaultSettings();
        Settings.ApplySettings();
        SetCharacters();
        SetStory();
    }

    static public void LoadGame()
    {
        LoadAll();
    }

    static public void LoadAll()
    {
        Settings.LoadSettings();
        Settings.ApplySettings();
        LoadCharacters();
        LoadStory();
    }

    static public void SetPlayer(CharacterKey key){currPlayerKey = GetCharacterKey(key);}

    static public bool isEventDone(int id)
    {
        if(story != null && story.events.Count > 0)
            return story.events[id].done;
        return false;
    }
    static public Sprite GetSprite(ResourceFolder folder, string spriteName)
    {
        string path = Enum.GetName(typeof(ResourceFolder), folder) + "/" + spriteName;
        Sprite sprite = Resources.Load<Sprite>(path);
        if(sprite == null)
        {
            path = Enum.GetName(typeof(ResourceFolder), folder) + "/default";
            sprite = Resources.Load<Sprite>(path);
        }
        return sprite;
    }
    static public string GetCharacterKey(CharacterKey chKey){return Enum.GetName(typeof(CharacterKey), chKey);}
    static public Character GetCharacter(CharacterKey chKey){return characters[GetCharacterKey(chKey)];}
    static public Character GetCharacter(string chKey) { return characters[chKey]; }

    static public void SetCharacters()
    {
        characters = new Dictionary<string, Character>();
        characters.Add(GetCharacterKey(CharacterKey.Player), new Player("Zien", GetCharacterKey(CharacterKey.Player), Status.alive, "avatar1"));
        characters.Add(GetCharacterKey(CharacterKey.DrBlack), new NPC("Dr.Black", GetCharacterKey(CharacterKey.DrBlack), Status.alive, "avatar2"));
        SetPlayer(CharacterKey.Player);
    }

    static public void SetStory()
    {
        story = new Story();
        List<Page> pages;
        Dictionary<string, int> statsToAdd;
        Dictionary<string, int> relationsToAdd;
        List<Route> routes;


        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Я не знаю как поступить что же делать??? Думай,думай...", GetCharacterKey(CharacterKey.Player)));
        pages.Add(new Page("Пора принимать решение Зиен... Каким путем вы пойдте?", GetCharacterKey(CharacterKey.DrBlack)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.stress), 4);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.strength), 3);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.intellect), -2);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.observe), 2);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.DrBlack), -2);

        routes.Add(new Route("Уничтожил компанию", "Соврать и уничтожить компанию", 1, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.stress), 2);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.strength), -1);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.intellect), 5);
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.observe), 5);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.DrBlack), 2);

        routes.Add(new Route("Поддержал компанию", "Я продолжу работать на компанию", 2, null, statsToAdd, relationsToAdd));

        //Event 0 Add
        Event ev0 = new Event(0, "Beginning",pages, routes);
        story.events.Add(ev0);


        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Да конечно, я сделаю все что в моих силах... На следующий день ты сжигаешь все что смог найти", GetCharacterKey(CharacterKey.Player)));
        pages.Add(new Page("Все теперь надо сделать вид что это был незапланированный пожар, расскажу об этом прессе", GetCharacterKey(CharacterKey.Player)));
        pages.Add(new Page("Я слышал что вы высказались по поводу наших секретных материалов пусть и сгоревших прессе? Думаю это станет весомым аргументом для Вашего увольнения", GetCharacterKey(CharacterKey.DrBlack)));
        pages.Add(new Page("Прошел месяц с моего увольнения я нашел множество единомышленников. Технологие до добра не доведут... Что то странное начинает происходить в городе...", GetCharacterKey(CharacterKey.Player)));

        //Routes
        routes = new List<Route>();
        routes.Add(new Route("Уничтожитель", "Я уничтожу их", 3, null, null, null));

        //Event 1 Add
        Event ev1 = new Event(1, "Destroy Comp", pages, routes);
        story.events.Add(ev1);

        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Да конечно, я сделаю все что в моих силах... Упорная работа приводит к невероятному успеху", GetCharacterKey(CharacterKey.Player)));
        pages.Add(new Page("Успехи просто потрясающие! Думаю это станет весомым аргументом для Вашего повышения, не против захода в совет директоров?", GetCharacterKey(CharacterKey.DrBlack)));
        pages.Add(new Page("Уже месяц как я один из директоров Шайда компани... Начинают происходить странные вещи", GetCharacterKey(CharacterKey.Player)));

        //Routes
        routes = new List<Route>();
        routes.Add(new Route("Спаситель", "Я спасу компанию", 3, null, null, null));

        //Event 2 Add
        Event ev2 = new Event(2, "Company leader", pages, routes);
        story.events.Add(ev2);

        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Что же будет дальше...", GetCharacterKey(CharacterKey.Player)));

        //Event 3 Add
        Event ev3 = new Event(3, "To be continued", pages, null);
        story.events.Add(ev3);
    }


    static public void SaveAll()
    {
        SaveCharacters();
        SaveStory();
    }

    public static void SaveCharacters()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + charactersFilename;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, characters);
        stream.Close();
    }

    public static void SaveStory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + storyFilename;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, story);
        stream.Close();
    }

    public static void LoadCharacters()
    {
        string path = Application.persistentDataPath + charactersFilename;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            characters = formatter.Deserialize(stream) as Dictionary<string, Character>;
            stream.Close();
        }
        else
        {
            Debug.LogError("Can't find characters file by " + path);
            return;
        }
    }

    public static void LoadStory()
    {
        string path = Application.persistentDataPath + storyFilename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            story = formatter.Deserialize(stream) as Story;
            stream.Close();
        }
        else
        {
            Debug.LogError("Can't find story file by " + path);
            return;
        }
    }
}
