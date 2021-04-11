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

    TextMeshProUGUI text;

    private void Awake() {
        ColorComment();
    }

    private void Update()
    {
        //count down time so that the comment will disappear at some point 
        lifetime -= 1 * Time.deltaTime;

        if (lifetime <= 0) {
            //subtract for bad
            if (commentNature == Nature.Bad)
            {
                minigameManager.CalcHP(-minigameManager.badCommentValue);
                Destroy(gameObject);
            }
        }
    }

    public void Ban() {
        //ban the bad comment
        if (commentNature == Nature.Bad) {
            Destroy(gameObject);
        }
    }

    public void Highlight() {
        //add HP for good comments when you highlighted them
        if (commentNature == Nature.Good) {
            minigameManager.CalcHP(minigameManager.goodCommentValue);
            Destroy(gameObject);
        }

        if (commentNature == Nature.Counter) {
            minigameManager.CalcHP(minigameManager.counterValue);
            Destroy(gameObject);
        }
    }

    void ColorComment() {

        //Make red
        if (commentNature == Nature.Bad || commentNature == Nature.Ruminate)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.color = Color.red;
        }   

        //make green
        if (commentNature == Nature.Good || commentNature == Nature.Counter) {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.color = Color.green;
        }
    }
}
