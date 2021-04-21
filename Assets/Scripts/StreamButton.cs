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

    private void Start()
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
        Debug.Log("Current placeholder text is: " + buttonComment.Text);
    }

    public void SetButtonComment(Comment otherComment) {

        Debug.Log("Called SetButtonComment");
        //copy the values over to this button's buttonComment
        buttonComment.Text = otherComment.Text;
        buttonComment.nature = otherComment.nature;
        buttonComment.speaker = otherComment.speaker;
        buttonComment.Phase = otherComment.Phase;
        Debug.Log(buttonComment.Text);//the real question is, why are we not printing this? Is the function broken?
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
