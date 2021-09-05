using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;

public class GameController : MonoBehaviour
{
    //To modify my UI through script, I need a reference.
    [Header("UI Settings")]
    public Text GemScoreText;
    public Text TimerText;
    public Text TutorialText;
    public Text DeathText;
    public GameObject AudioWin;
    public GameObject AudioLose;
    public GameObject MainAudio;
    public GameObject gameOverUI;
    public GameObject nextLevelUI;

    //Bools to determine certain criterias.
    private bool isRunning = false;
    private bool nextLevel, tutorialText, levelComplete, textLevel = false;
    public bool GemDeath = false;
    private bool LostGame = false;

    //Getting the timer to run properly.
    private float currentTime;

    //Knowing which scenes are text in order to make next level = true.
    public string[] textScenes;
    public int numberOfTextScenes;

    //Determine how to end the game. The gem count.
    private int score = 0;
    private int count = 5;

    //Int on index to keep track of the current level.
    private int nextSceneToLoad = 0;

    void Awake()
    {
        //In order for a level to run, this needs to run.
        TimerStart();
        //Int of current level.
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        //Setting certain bools to false for game to proceed.
        nextLevel = levelComplete = false;
        //Level one tutorial text. The rest of the levels will cause this to be false.
        //tutorialText = true;
        //Figuring out if it's a text scene. If so, make level beatable.
        for (int i = 0; i < numberOfTextScenes; i++)
        {
            if (SceneManager.GetActiveScene().name == textScenes[i])
            {
                textLevel = true;
            }
        }
        //Checks score to see if the level is beat already.
        UpdateScore();
    }
    public void TimerStart()
    {
        //Makes the timer run and time itself to flow at regular speed.
        Time.timeScale = 1f;
        isRunning = true;
    }
    public void NextLevel()
    {
        //Proceed to the next level.
        SceneManager.LoadScene(nextSceneToLoad);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        //Restart the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    void Update()
    {
        //Allows the game to have a timer.
        if (isRunning == true)
        {
            currentTime += Time.deltaTime;
            TimerText.text = ($"Timer: {currentTime.ToString("00:00.00")}");
        }
        if(nextLevel && Input.GetKeyDown(KeyCode.N) && !textLevel)
        {
            NextLevel();
        }
        if (nextLevel && Input.GetKeyDown(KeyCode.R) && !textLevel)
        {
            Restart();
        }
        if (LostGame && Input.GetKeyDown(KeyCode.R) && !textLevel)
        {
            Restart();
        }
        if (LostGame && Input.GetKeyDown(KeyCode.Q) && !textLevel)
        {
            Application.Quit();
        }
        //If player stays stuck on the same level for 15 minutes, end the game (lost).
        if (currentTime >= 900)
        {
            EndGame();
        }
    }
    public void AddScore(int newScoreValue)
    {
        //Keeps track of the score in game.
        score += newScoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        //Updates the game UI that's keeping track of the score. If the score is equal to count, player passed.
        GemScoreText.text = ($"Gem Score: {+score}/{count}");
        if (score == count)
        {
            GameWon();
        }
    }
    public void GameWon()
    {
        //If player beat the level, make it possible to restart the level and next level becomes active.
        MainAudio.GetComponent<AudioSource>().Stop();
        AudioWin.GetComponent<AudioSource>().Play();
        isRunning = false;
        nextLevel = true;
        nextLevelUI.SetActive(true);
    }
    public void EndGame()
    {
        //If player died after getting the gem, make this obsolete.
        if (nextLevel == false)
        {
            LostGame = true;
            gameOverUI.SetActive(true);
            MainAudio.GetComponent<AudioSource>().Stop();
            AudioLose.GetComponent<AudioSource>().Play();
            if (GemDeath)
            {
                StartCoroutine(ExecuteAfterTime(1.01f));
                DeathText.text = "Gem left the playing area";
            }
            else if (currentTime >= 900)
            {
                StartCoroutine(ExecuteAfterTime(1.01f));
                DeathText.text = "You ran out of time";
            }
            else
            {
                StartCoroutine(ExecuteAfterTime(1.01f));
                DeathText.text = "You died";
            }
        }
        
    }
    //Enumerator used to stop time after player lost, exactly 1.01 seconds after time expired or death or gem destroyed.
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}