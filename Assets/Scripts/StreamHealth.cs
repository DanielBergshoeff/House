using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script should sit on the minigame manager alongside minigame manager script

public class StreamHealth : MonoBehaviour
{
    public MinigameManager minigameManager;
    public Slider slider;
    public int health = 10;


    private int _phasePoints; //the total points that will be added to the total health at the end of a round

    public void Subtract(int value) {
        _phasePoints -= value;
    }

    public void Add(int value) {
        _phasePoints += value;
    }

    public void CalcHP()
    {
        //check if yellows second button comments
        if (minigameManager.yellowChild1.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Bad) { //if yellows first comment was a bad
            Subtract(minigameManager.badCommentValue);
        }

        //check if there are ruminate comments
        if (minigameManager.yellowChild1.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate) { //if yellows first comment was a rumination
            Subtract(minigameManager.ruminateValue);
        }

        //check yellows first button comment
        if (minigameManager.yellowChild2.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Bad)
        { //if yellows first comment was a bad
            Subtract(minigameManager.badCommentValue);
        }

        //check if there are ruminate comments
        if (minigameManager.yellowChild2.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate)
        { //if yellows first comment was a rumination
            Subtract(minigameManager.ruminateValue);
        }


        health += _phasePoints;
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
