using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //Checking if the game is paused.
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void Update()
    {
        //When you press escape, pause the game. Press it again to resume the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Resume the game, and reset the time to normal.
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        //Time.timeScale is to freeze time or advance it.
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        //Resetting the scale back to normal, before loading the main menu.
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        //Quit the game.
        Time.timeScale = 1f;
        Application.Quit();
    }
}
