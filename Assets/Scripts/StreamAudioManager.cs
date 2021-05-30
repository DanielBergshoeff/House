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
    public AudioSource audioSource;

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

    private void Start()
    {
        //why did this not work?
        //AudioSource audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip sound)
    {   

        audioSource.PlayOneShot(sound);
    }
}