using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//this script sits on both buttons that display Yellows comments

public class StreamButton : MonoBehaviour
{
    public MinigameManager minigameManager;
    public StreamHealth streamHealth;

    [Header("Keep this empty")]
    [HideInInspector]
    public Comment buttonComment;


    private void Awake()
    {
        ////this makes sure console dont go flippin if there aint no comment
        //buttonComment = minigameManager.emptyComment;
    }

    private void Update()
    {
        if (buttonComment != null) { //if you don't use this, things will work, but console will display a lot of errors
            //set text
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = buttonComment.Text;

            //set pos color
            if (buttonComment.nature == Comment.Nature.Good || buttonComment.nature == Comment.Nature.Counter) {
                gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.Lerp(minigameManager.positiveColor, minigameManager.neutralColor, (1f / minigameManager.lifetime+minigameManager.lerpModefier) * minigameManager.remainingLifetime);
                //set the sound
            }

            //set neg color
            if (buttonComment.nature == Comment.Nature.Bad || buttonComment.nature == Comment.Nature.Ruminate) {
                gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.Lerp(minigameManager.negativeColor, minigameManager.neutralColor, (1f/minigameManager.lifetime+minigameManager.lerpModefier)*minigameManager.remainingLifetime);
            }

            //set neutral color
            if (buttonComment.nature == Comment.Nature.Neutral) {
                gameObject.GetComponentInChildren<TextMeshProUGUI>().color = minigameManager.neutralColor;
            }
        }
    }

    public void CalcDMG() { 
        //calculate negative damage
    }

    //button methods
    public void CheckComment() {
        
        //add points if good comment
        if (buttonComment.nature == Comment.Nature.Good && buttonComment.interactedWith == false) {
            streamHealth.Add(minigameManager.goodCommentValue);
            PlayTooltip("Highlighted!", minigameManager.positiveColor);
            minigameManager.audioManager.playSound(minigameManager.audioManager.goodSound);
            buttonComment.interactedWith = true;

            //play the right effect
            Instantiate(minigameManager.goodCommentClickEffect, transform);
        }
        
        //add points if counter comment
        if (buttonComment.nature == Comment.Nature.Counter && buttonComment.interactedWith == false)
        {
            streamHealth.Add(minigameManager.counterValue);
            PlayTooltip("Counter found!", minigameManager.positiveColor);
            minigameManager.audioManager.playSound(minigameManager.audioManager.counterSound);
            buttonComment.interactedWith = true;
        }


        //neutralize bad comment to neutral if bad
        if (buttonComment.nature == Comment.Nature.Bad && buttonComment.interactedWith == false)
        {
            buttonComment = minigameManager.emptyComment;
            PlayTooltip("Neutralized!", minigameManager.neutralColor);
            minigameManager.audioManager.playSound(minigameManager.audioManager.badSound);
            buttonComment.interactedWith = true;
        }

        //VIEWER BUTTONS FUNCTIONALITY

        //Counters:
        //check all current yellowsComments buttonComments' nature
        //if it was ruminate, neutralize that by turning its nature to neutral


        //check yellow child 1
        if (buttonComment.nature == Comment.Nature.Counter && buttonComment.interactedWith == false) { //if this buttons buttonComment was a counter
            if (minigameManager.yellowChild1.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate) {
                minigameManager.yellowChild1.GetComponentInChildren<StreamButton>().buttonComment = minigameManager.emptyComment;
                PlayTooltip("Counter found!", minigameManager.positiveColor);
                Debug.Log("Comment clicked was a counter on yellow comment 1");
                buttonComment.interactedWith = true;
            }
        }

        //check yellow child 2
        if (buttonComment.nature == Comment.Nature.Counter && buttonComment.interactedWith == false)
        { //if this buttons buttonComment was a counter
            if (minigameManager.yellowChild2.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate) {
                minigameManager.yellowChild2.GetComponentInChildren<StreamButton>().buttonComment = minigameManager.emptyComment;
                PlayTooltip("Counter found!", minigameManager.positiveColor);
                Debug.Log("Comment clicked was a counter on yellow comment 2");
                buttonComment.interactedWith = true;
            }
        }

        //RUMINATES:
        if (buttonComment.nature == Comment.Nature.Ruminate) { //if the nature of this comment was ruminate,
            PlayTooltip("Cannot ban, find a counter!", minigameManager.negativeColor);
            minigameManager.audioManager.playSound(minigameManager.audioManager.ruminateSound);
            Debug.Log("Find a counter comment!"); //hint the player in a text to look for the right counter
        }

         void PlayTooltip(string text, Color color){
            minigameManager.tooltip.transform.position = gameObject.transform.position; //then set the pos of tooltip base off the comment you clicked
            minigameManager.tooltip.GetComponentInChildren<TextMeshProUGUI>().text = text; //set text
            minigameManager.tooltip.GetComponentInChildren<TextMeshProUGUI>().color = color; //set color
            minigameManager.tooltipCanvas.GetComponent<Animator>().SetTrigger("play");
            }
    }

}
