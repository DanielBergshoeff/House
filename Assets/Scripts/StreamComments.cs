using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //do we need this? (optimizse)


public class StreamComments : MonoBehaviour
{
    //create a reference to the minigame manager to allow conversation between scripts
    public MinigameManager minigameManager;

    public enum Nature {Good, Bad, Counter, Ruminate, Neutral};
    [Tooltip("Set the nature of this comment. Is it a good or bad one, or does it either an indication of a counter to another sentence or rumination?")]
    public Nature commentNature;
    //optimisation tip: can we prevent every sentence having to make a new enum set?

    [Tooltip("Set how long the comment will appear on screen for.")]
    public float lifetime = 4;

    private void Update()
    {
        //count down time so that the comment will disappear at some point 
        lifetime -= 1 * Time.deltaTime;

        DoThing();
        
    }

    //int DoThing() {
    //    if (lifetime <= 0)
    //    {
    //        if (commentNature == Nature.Bad)
    //        {
    //            minigameManager.CalcHP(5);
    //        }
    //    }
    //}
}
