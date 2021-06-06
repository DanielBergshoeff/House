using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//set this script on an object with a sound source so that it can keep feeding the component with new clips
//so, have this script house audio manager,
//tell in start getcomponent audio manager thats on this same object
//give it public audio clips
//then have the audio source play the one shots

public class StreamAudioManager : MonoBehaviour  
{
    public AudioSource mainSource;
    public AudioSource subSource;
    public bool fadingIn = false;

    [Header("Nature sounds")]
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip counterSound;
    public AudioClip ruminateSound;

    [Header("Other sounds")]
    public AudioClip hoverComment;
    public AudioClip countdownTick;
    public AudioClip countdownEnd;

    [Header("Soundtracks")]
    public AudioClip mainHPTrack;
    public AudioClip lowHPTrack;

    private void Awake()
    {
        mainSource.clip = mainHPTrack;
        mainSource.Play();

        subSource.clip = lowHPTrack;
        subSource.volume = 0;
        subSource.Play();
    }


    public void playSound(AudioClip sound) {
        mainSource.PlayOneShot(sound);
    }

    public void fadeInLowHPTrack() {
        subSource.volume = 1;
    }

    public void fadeOutLowHPTrack() {
        //Debug.Log("Fading out");
        subSource.volume = 0;
    }

    public void playHover() {
        mainSource.PlayOneShot(hoverComment);
    }
}