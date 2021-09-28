using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour
{
    public Dropdown dropdown;
    
    private void Start()
    {
        DropDownFill();
    }

    public void ReastartBtn()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Global.charactersFilename;
        File.Delete(path);
        path = Application.persistentDataPath + Global.storyFilename;
        File.Delete(path);
        SceneManager.LoadScene("MainMenu");
    }

    private void DropDownFill()
    {
        dropdown.AddOptions(new List<string>(Enum.GetNames(typeof(Language))));
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged();
        });
        dropdown.value = dropdown.options.FindIndex(option => option.text == Settings.language);
    }

    public void DropdownValueChanged()
    {
        Settings.SetLanguage(dropdown.itemText.text);
    }
}
