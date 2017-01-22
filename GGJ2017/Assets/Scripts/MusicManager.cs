using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] themes;
    int index = 0;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = themes[index];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            index = (index + 1) % themes.Length;
            audioSource.clip = themes[index];
            audioSource.Play();
        }
    }
}
