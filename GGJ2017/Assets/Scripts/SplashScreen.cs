using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SplashScreen : MonoBehaviour
{
    public MovieTexture screenTexture;
    private AudioSource audioSource;

    void Start()
    {
        GetComponent<RawImage>().texture = screenTexture as MovieTexture;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = screenTexture.audioClip;
        screenTexture.Play();
        audioSource.Play();
    }

    void Update()
    {
        if (!screenTexture.isPlaying)
        {
            SceneManager.LoadScene(1);
        }
    }
}
