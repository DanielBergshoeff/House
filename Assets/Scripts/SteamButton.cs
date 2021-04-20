using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script sits on both buttons that display Yellows comments

public class SteamButton : MonoBehaviour
{
    private Comment buttonComment;

    private void Update()
    {
        //set pos color
        //set neg color
        //set neutral color

        //set text
    }

    public void SetButtonComment(Comment otherComment) { 
        //copy the values over to this button's buttonComment
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
