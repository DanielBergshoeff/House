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
        health -= value;
    }

    public void Add(int value) {
        health += value;
    }

    public void CalcHP()
    {
        //check viewers comments for bad comments
        int childCountB = minigameManager.viewerComments.transform.childCount;
        for (int i = 0; i < childCountB; i++)
        {
            if (minigameManager.viewerComments.transform.GetChild(i).GetComponent<StreamButton>().buttonComment.nature == Comment.Nature.Bad) {
                Subtract(minigameManager.badCommentValue);
                Debug.Log("Viewer comment" + minigameManager.viewerComments.transform.GetChild(i).GetComponent<StreamButton>().buttonComment.Text + " was bad");
            }
        }

        


        //health += _phasePoints;
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
