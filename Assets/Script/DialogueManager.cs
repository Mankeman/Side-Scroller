using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject wakeUpButton;
    public GameObject dialogueBox;
    public GameObject noButton;
    public GameController gameController;
    public Text continueText;
    public Text dialogueText;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        wakeUpButton.SetActive(false);
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 1)
        {
            if(SceneManager.GetActiveScene().name == "Level21")
            {
                continueText.text = "Okay";
            }
            else
            {
                continueText.text = "Yes";
                noButton.SetActive(true);
            }
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
    public void EndDialogue()
    {
        gameController.NextLevel();
    }
    public void MainMenu()
    {
        gameController.MainMenu();
    }
}
