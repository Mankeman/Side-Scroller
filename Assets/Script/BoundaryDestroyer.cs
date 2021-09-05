using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour
{
    //Access the game controller to end the game
    public GameController gameControllerScript;

    private void Awake()
    {
        //Find the game controller and set it up
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameControllerScript = gameControllerObject.GetComponent<GameController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Touch the player and you lose.
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Animator>().SetBool("Dead", true);
            gameControllerScript.EndGame();
        }
        //Touch the gem and you lose.
        if (other.gameObject.tag == "Gem")
        {
            Destroy(other.gameObject);
            gameControllerScript.GemDeath = true;
            gameControllerScript.EndGame(); 
        }
    }
}
