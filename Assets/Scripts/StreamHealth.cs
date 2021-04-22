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
        Debug.Log("Gonna subtract " + value);
        health -= value;
    }

    public void Add(int value) {
        health += value;
    }

    public void CalcHP()
    {
        
        int childCountB = minigameManager.viewerComments.transform.childCount;
        for (int i = 0; i < childCountB; i++)
        {
            //subtract viewers bad comments
            if (minigameManager.viewerComments.transform.GetChild(i).GetComponent<StreamButton>().buttonComment.nature == Comment.Nature.Bad)
            {
                Subtract(minigameManager.badCommentValue);
                Debug.Log("a viewer comment was bad");
            }
        }

        //subtract yellow bad comments
        if (minigameManager.yellowChild1.transform.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Bad) {
          Debug.Log("yellow child 1 is bad");
          Subtract(minigameManager.badCommentValue);
        }
        if (minigameManager.yellowChild2.transform.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Bad) {
          Debug.Log("yellow child 2 us bad");
          Subtract(minigameManager.badCommentValue);
        }

        //subtract yellow  ruminations
        if (minigameManager.yellowChild1.transform.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate) {
            Debug.Log("yellow child 1 is ruminate");
            Subtract(minigameManager.ruminateValue);
        }
        if (minigameManager.yellowChild2.transform.GetComponentInChildren<StreamButton>().buttonComment.nature == Comment.Nature.Ruminate) {
            Debug.Log("yellow child 2 is ruminate");
            Subtract(minigameManager.ruminateValue);
        }

        //health += _phasePoints;
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
