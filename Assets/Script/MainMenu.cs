using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Loads the next scene in the index.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        //If the game is built, automatically leaves.
        Application.Quit();
    }
}
