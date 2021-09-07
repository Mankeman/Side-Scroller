using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject normBird;
    public GameObject colorBird;
    public GameObject gemContainer;
    public GameObject leftContainer;
    public GameObject rightContainer;
    public Text birdDeathCount;
    public AudioSource crying;
    public SoundManager soundManager;
    public int birdDeath = 0;

    public void Update()
    {
        if (rightContainer.transform.childCount < 2)
        {
            SpawnBirdRight();
        }
        if (leftContainer.transform.childCount < 2)
        {
            SpawnBirdLeft();
        }
        if (birdDeath == 5)
        {
            SpawnGems();
        }
        UpdateDeathCount();
    }
    private void SpawnBirdLeft()
    {
        Instantiate(normBird, leftContainer.transform.position, leftContainer.transform.rotation, leftContainer.transform);
        birdDeath += 1;
    }
    private void SpawnBirdRight()
    {
        Instantiate(colorBird, rightContainer.transform.position, rightContainer.transform.rotation, rightContainer.transform);
        birdDeath += 1;
    }
    private void SpawnGems()
    {
        soundManager.SoundEffect(crying, 0.75f, 1.25f);
        gemContainer.SetActive(true);
    }
    private void UpdateDeathCount()
    {
        birdDeathCount.text = $"Bird Deaths: {birdDeath.ToString()}/5";
    }
}
