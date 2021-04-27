using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour {
    public AmbientSoundSetting[] soundSettings;
    public int soundIndex;
    public float minFoleyFrequency = 1;
    public float blendSpeed = 0.3f;
    private int previousIndex;
    private float blendInVelocity;
    private float blendOutVelocity;
    // SurfaceSounds
    // PlayerUnderWater
    // SubmarineUnderWater
    // CaveSounds
    // CaveSounds2
    // CaveSounds3
    
    // Start is called before the first frame update
    void Start() {
        Invoke("PlayFoley", Random.Range(minFoleyFrequency, soundSettings[soundIndex].foleyFrequency)); 
    }

    // Update is called once per frame
    void Update() {
        PlayAmbientSound();
    }

    public void SetSoundScape(int scapeIndex) {
        CancelInvoke();
        previousIndex = scapeIndex;
        soundIndex = scapeIndex;
        Invoke("PlayFoley", Random.Range(minFoleyFrequency, soundSettings[soundIndex].foleyFrequency));
    }

    void PlayAmbientSound() {
        if(soundSettings[soundIndex].soundSource.clip == null) {
            return;
        }

        if(!soundSettings[soundIndex].soundSource.isPlaying) {
            soundSettings[soundIndex].soundSource.Play();
        }

        soundSettings[soundIndex].foleySource.volume = Mathf.SmoothDamp(
            soundSettings[soundIndex].foleySource.volume,
            soundSettings[soundIndex].foleyVolume,
            ref blendInVelocity,
            blendSpeed
        );

        soundSettings[previousIndex].foleySource.volume = Mathf.SmoothDamp(
            soundSettings[previousIndex].foleySource.volume,
            0,
            ref blendOutVelocity,
            blendSpeed
        );
    }

    void PlayFoley() {
        AudioClip clip = soundSettings[soundIndex].foleySounds[Random.Range(0, soundSettings[soundIndex].foleySounds.Length)];
        soundSettings[soundIndex].foleySource.PlayOneShot(clip);
        Invoke("PlayFoley", Random.Range(minFoleyFrequency, soundSettings[soundIndex].foleyFrequency));
    }
}
