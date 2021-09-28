using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SettingsObj
{
    public bool isVolumeOn;
    public bool isHintsEnable;
    public string language;

    public SettingsObj(bool isVolumeOn, bool isHintsEnable, string language)
    {
        this.isVolumeOn = isVolumeOn;
        this.isHintsEnable = isHintsEnable;
        this.language = language;
    }
}

static public class Settings
{
    static public bool isVolumeOn;
    static public bool isHintsEnable;
    static public string language;

    static public void SetDefaultSettings()
    {
        isVolumeOn = true;
        isHintsEnable = true;
        language = Enum.GetName(typeof(Language), Language.russian);
    }

    static public void SetHints(bool value)
    {
        isHintsEnable = value;
        SaveSettings();
        ApplySettings();
    }

    static public void SetVolume(bool value)
    {
        isVolumeOn = value;
        SaveSettings();
        ApplySettings();
    }

    static public void SetLanguage(string new_language)
    {
        language = new_language;
        SaveSettings();
        ApplySettings();
    }

    static public void SaveSettings()
    {
        SettingsObj settings = new SettingsObj(isVolumeOn, isHintsEnable, language);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.yuda";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, settings);
        stream.Close();
    }

    static public void LoadSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Global.settingsFilename;
        FileStream stream = new FileStream(path, FileMode.Open);
        SettingsObj settings = (SettingsObj)formatter.Deserialize(stream);
        isVolumeOn = settings.isVolumeOn;
        isHintsEnable = settings.isHintsEnable;
        language = settings.language;

        stream.Close();
    }

    static public void ApplySettings()
    {
        if(isVolumeOn)
            AudioListener.volume = 1f;
        else
            AudioListener.volume = 0f;


    }
}
