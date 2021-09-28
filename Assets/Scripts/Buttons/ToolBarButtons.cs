using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarButtons : MonoBehaviour
{
    public GameObject plot_screen;
    public GameObject char_screen;
    public GameObject conn_screen;
    public GameObject story_screen;
    public GameObject settings_screen;

    Button settings_btn;
    Button plot_btn;
    Button story_btn;
    Button char_btn;
    Button conn_btn;

    Sprite defSettings, tapSettings, defPlot, tapPlot, defStory, tapStory, defChar, tapChar, defSocial, tapSocial;

    private void Start()
    {
        settings_btn = transform.GetChild(0).GetComponent<Button>();
        plot_btn = transform.GetChild(1).GetComponent<Button>();
        story_btn = transform.GetChild(2).GetComponent<Button>();
        char_btn = transform.GetChild(3).GetComponent<Button>();
        conn_btn = transform.GetChild(4).GetComponent<Button>();

        defSettings = Global.GetSprite(ResourceFolder.ToolBar, "settings");
        tapSettings = Global.GetSprite(ResourceFolder.ToolBar, "settings_tap");

        defPlot = Global.GetSprite(ResourceFolder.ToolBar, "plot");
        tapPlot = Global.GetSprite(ResourceFolder.ToolBar, "plot_tap");

        defStory = Global.GetSprite(ResourceFolder.ToolBar, "story");
        tapStory = Global.GetSprite(ResourceFolder.ToolBar, "story_tap");

        defChar = Global.GetSprite(ResourceFolder.ToolBar, "char");
        tapChar = Global.GetSprite(ResourceFolder.ToolBar, "char_tap");

        defSocial = Global.GetSprite(ResourceFolder.ToolBar, "social");
        tapSocial = Global.GetSprite(ResourceFolder.ToolBar, "social_tap");

        story_btn.image.sprite = tapStory;
    }

    public void SettingsBtn()
    {
        plot_screen.SetActive(false);
        char_screen.SetActive(false);
        conn_screen.SetActive(false);
        story_screen.SetActive(false);
        settings_screen.SetActive(true);

        settings_btn.image.sprite = tapSettings;
        plot_btn.image.sprite = defPlot;
        story_btn.image.sprite = defStory;
        char_btn.image.sprite = defChar;
        conn_btn.image.sprite = defSocial;
    }

    public void StoryBtn()
    {
        plot_screen.SetActive(false);
        char_screen.SetActive(false);
        conn_screen.SetActive(false);
        story_screen.SetActive(true);
        settings_screen.SetActive(false);

        settings_btn.image.sprite = defSettings;
        plot_btn.image.sprite = defPlot;
        story_btn.image.sprite = tapStory;
        char_btn.image.sprite = defChar;
        conn_btn.image.sprite = defSocial;
    }

    public void PlotBtn()
    {
        plot_screen.GetComponent<PlotScreen>().UpdateData();
        plot_screen.SetActive(true);
        char_screen.SetActive(false);
        conn_screen.SetActive(false);
        story_screen.SetActive(false);
        settings_screen.SetActive(false);

        settings_btn.image.sprite = defSettings;
        plot_btn.image.sprite = tapPlot;
        story_btn.image.sprite = defStory;
        char_btn.image.sprite = defChar;
        conn_btn.image.sprite = defSocial;

    }

    public void CharBtn()
    {
        char_screen.GetComponent<CharacterScreen>().UpdateData();
        plot_screen.SetActive(false);
        char_screen.SetActive(true);
        conn_screen.SetActive(false);
        story_screen.SetActive(false);
        settings_screen.SetActive(false);

        settings_btn.image.sprite = defSettings;
        plot_btn.image.sprite = defPlot;
        story_btn.image.sprite = defStory;
        char_btn.image.sprite = tapChar;
        conn_btn.image.sprite = defSocial;
    }

    public void ConnBtn()
    {
        conn_screen.GetComponent<ConnectionsScreen>().UpdateData();

        plot_screen.SetActive(false);
        char_screen.SetActive(false);
        conn_screen.SetActive(true);
        story_screen.SetActive(false);
        settings_screen.SetActive(false);

        settings_btn.image.sprite = defSettings;
        plot_btn.image.sprite = defPlot;
        story_btn.image.sprite = defStory;
        char_btn.image.sprite = defChar;
        conn_btn.image.sprite = tapSocial;
        
    }
}
