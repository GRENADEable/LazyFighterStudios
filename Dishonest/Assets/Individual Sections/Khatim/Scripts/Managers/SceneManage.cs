using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("DishonestGameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Game Closed");
    }
}
