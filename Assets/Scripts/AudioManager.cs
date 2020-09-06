﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip PlayerRun;
    public AudioClip PlayerWalk;
    public AudioClip Bounce;
    public AudioClip Impact;
    public AudioClip Chair;
    public AudioClip Glider;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
