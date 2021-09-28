using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private void Start()
    {
        string settingsPath = Application.persistentDataPath + Global.settingsFilename;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(settingsPath, FileMode.OpenOrCreate);
        bool isVolumeOn = (bool)formatter.Deserialize(stream);
        SetAudio(isVolumeOn);
        stream.Close();
    }
    void SetAudio(bool enabled)
    {
        if (enabled)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }
    public void Tapped()
    {
        string storyPath = Application.persistentDataPath + Global.storyFilename;
        string charPath = Application.persistentDataPath + Global.charactersFilename;

        if (File.Exists(storyPath) && File.Exists(charPath))
            Continue();
        else
            NewGame();
    }

    private void NewGame()
    {
        Global.NewGame();
        SceneManager.LoadScene("Game");
    }

    private void Continue()
    {
        Global.LoadGame();
        SceneManager.LoadScene("Game");
    }

}
