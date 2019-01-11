using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    public GameObject bgPanel;
    public GameObject aboutBgPanel;

    void Awake()
    {
        bgPanel.SetActive(true);
        aboutBgPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManage.instance.NewGame();
        AudioManager.instance.StopAudio();
        AudioManager.instance.SinglePlayerAudio();
    }

    public void AboutToMainMenu()
    {
        aboutPanel.SetActive(false);
        aboutBgPanel.SetActive(false);
        bgPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
    }

    public void SettingsToMainMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        SceneManage.instance.Exit();
    }

    public void SettingsPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void AboutPanel()
    {
        mainMenuPanel.SetActive(false);
        bgPanel.SetActive(false);
        aboutBgPanel.SetActive(true);
        aboutPanel.SetActive(true);
    }
}
