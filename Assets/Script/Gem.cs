using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    //The score you get when you grab a gem.
    public int scoreValue = 1;
    //You need to reference the game controller script later on.
    public GameController gameControllerScript;
    public GameObject itemFeedback;
    public Rigidbody2D rb;
    void Start()
    {
        //Checking if there's a Game Controller in the level already.
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        rb = GetComponent<Rigidbody2D>();

        //If a Game Controller is not null (is found), then grab that component.
        if (gameControllerObject != null)
        {
            gameControllerScript = gameControllerObject.GetComponent<GameController>();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //If the gem collides with player, increase score and destroy it.
        if (other.gameObject.tag == "Player")
        {
            Instantiate(itemFeedback, this.transform.position, this.transform.rotation);
            gameControllerScript.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
