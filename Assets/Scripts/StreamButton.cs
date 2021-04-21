using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//this script sits on both buttons that display Yellows comments

public class StreamButton : MonoBehaviour
{
    public MinigameManager minigameManager;

    [Header("Keep this empty")]
    [HideInInspector]
    public Comment buttonComment;

    private void Awake()
    {
        //this makes sure console dont go flippin if there aint no comment
        buttonComment = minigameManager.emptyComment;
    }

    private void Update()
    {
        if (buttonComment.nature == Comment.Nature.Good || buttonComment.nature == Comment.Nature.Counter)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = minigameManager.positiveColor;
        }

        //set neg color
        //set neutral color

        //set text
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = buttonComment.Text;
    }

    public void SetButtonComment(Comment otherComment) {

        //copy the values over to this button's buttonComment
        Debug.Log("Button text before overwrite: " + buttonComment.Text + ". Other comment text: " + otherComment.Text);
        buttonComment.Text = otherComment.Text;
        buttonComment.nature = otherComment.nature;
        buttonComment.speaker = otherComment.speaker;
        buttonComment.Phase = otherComment.Phase;
    }

    public void CalcDMG() { 
        //calculate negative damage
    }

    //button methods
    public void CheckComment() { 
        //add points if positive comment
        //neutralize bad comment to neutral if bad

        //VIEWER BUTTONS FUNCTIONALITY
        
        //Counters:
        //check if this buttons buttonComment was a counter
        //if so, check all current yellowsComments buttonComments' nature
        //if it was ruminate, neutralize that by turning its nature to neutral

        //Ruminates:
        //if the nature of this comment was ruminate,
        //hint the player in a text to look for the right counter
    }

}
