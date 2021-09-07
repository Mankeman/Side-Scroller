using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEverything : MonoBehaviour
{
    private GameController gameControllerScript;
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameControllerScript = gameControllerObject.GetComponent<GameController>();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Dead", true);
            gameControllerScript.EndGame();
        }
        else if (other.gameObject.tag == "Gem")
        {
            Destroy(other.gameObject);
            gameControllerScript.GemDeath = true;
            gameControllerScript.EndGame();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
