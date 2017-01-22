using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    public Animator anim;
    public AudioSource audioSrc_1;
    public AudioSource audioSrc_2;

    void Walk() {
        audioSrc_1.pitch = Random.Range(0.95f, 1.05f);
        audioSrc_1.Play();
    }

    void Shoot()
    {
        audioSrc_2.Play();
        StartCoroutine(Step());
    }

    IEnumerator Step()
    {
        var inf = anim.GetCurrentAnimatorStateInfo(0);
        while (anim.GetBool("plusFiring")) yield return null;
        audioSrc_2.Stop();
    }
}
