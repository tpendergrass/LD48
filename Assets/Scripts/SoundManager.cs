using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip[] clips;
    public AudioSource source;

    public void Play(int soundIndex) {
        source.PlayOneShot(clips[soundIndex]);
    }
}
