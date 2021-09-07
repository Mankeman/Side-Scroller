using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private float random;
    public void SoundEffect(AudioSource clip, float min, float max)
    {
        random = Random.Range(min, max);
        clip.pitch = random;
        clip.Play();
    }
}
