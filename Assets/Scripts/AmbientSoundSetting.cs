using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AmbientSoundSetting {
    public AudioSource soundSource;
    public AudioSource foleySource;
    public AudioClip environmentSound;
    public float foleyFrequency;
    public float foleyVolume;
    public AudioClip[] foleySounds;
    
}
