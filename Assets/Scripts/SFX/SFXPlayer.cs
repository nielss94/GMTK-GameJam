using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();    
    }

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
