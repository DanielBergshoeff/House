using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in order to 'skip' time, we simply cover up the stream inerface using the menu canvas,
//while the minigame behind it actually displays empty comments.

public class StreamSkipTime : MonoBehaviour
{
    public StreamMenuProtocol streamMenuProtocol;
    public MinigameManager minigameManager; //from this script well keep track what phase were in
    public int[] skipPhases;

    void Update()
    {
        for (int i = 0; i < skipPhases.Length; i++) { //go over all indicated phases to skip
            if (minigameManager.phase == skipPhases[i]) { //check if current phase is equal to the currently looked at indicated phases to skip
                streamMenuProtocol.SkipProtocol();
            }
        }

        //nor remove the screen again
        for (int i = 0; i < skipPhases.Length; i++)
        { //go over all indicated phases to skip
            if (minigameManager.phase == skipPhases[i]+1)
            { //check if current phase is equal to the currently looked at indicated phases to skip
                streamMenuProtocol.PlayProtocol();
            }
        }
    }
    //reset the protocol?
}
