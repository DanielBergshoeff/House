﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Comment", menuName = "Comment")]//put straight above class
public class Comment : ScriptableObject
{
    //enables selecting from phase and viewer
    public enum Speaker { Yellow, Viewer };
    public Speaker speaker;
    [Tooltip("Count starting from 0! You can also tell the phase from the first number of the node/comment name.")]
    public int Phase;
    public enum Nature { Neutral, Good, Bad, Counter, Ruminate };
    public Nature nature;
    public string Text = "Insert speaker's text";
}