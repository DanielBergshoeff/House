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
                gameObject.GetComponentInChildren<TextMeshProUGUI>().color = minigameManager.positiveColor;
             }

            //set neg color
            if (buttonComment.nature == Comment.Nature.Bad || buttonComment.nature == Comment.Nature.Ruminate)
            {
                gameObject.GetComponentInChildren<TextMeshProUGUI>().color = minigameManager.negativeColor;
            }

            //set neutral color
            if (buttonComment.nature == Comment.Nature.Neutral)
            {
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
        if (buttonComment.nature == Comment.Nature.Good) {
            streamHealth.Add(minigameManager.goodCommentValue);
            Debug.Log("Comment clicked was good");
        }

        //neutralize bad comment to neutral if bad
        if (buttonComment.nature == Comment.Nature.Bad)
        {
            buttonComment = minigameManager.emptyComment;
            Debug.Log("Comment clicked was bad");
        }

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
