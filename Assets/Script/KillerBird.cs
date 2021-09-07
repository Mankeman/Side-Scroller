using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBird : MonoBehaviour
{
    public int health = 100;
    private GameController gameControllerScript;
    public SoundManager soundManager;
    public AudioSource eagleSound;
    void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        eagleSound = GameObject.Find("AudioEagle").GetComponent<AudioSource>();
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
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        soundManager.SoundEffect(eagleSound, 0.9f, 1.10f);
        Destroy(gameObject);
    }
    private void GemDeath()
    {
        Destroy(gameObject);
    }
}
