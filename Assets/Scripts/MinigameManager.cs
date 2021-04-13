using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    [Tooltip("Insert whatever parent directly holds the 2 game objects that are Yellow's sentences.")]
    public GameObject yellowsComments;
    [Tooltip("Insert the parent that holds all the viewers comments.")]
    public GameObject viewerComments;
    
    public Slider slider;
    public int health = 10;

    public int badCommentValue = 1; //for some reason, negative numbers become positive when bounced between scripts. The - is now thrown in the StreamComments script.
    public int goodCommentValue = 1;
    public int ruminateValue = 2;
    public int counterValue = 2;
    [Tooltip("Set color for good and counter colors.")]
    public Color positiveColor;
    [Tooltip("Set color for bad and ruminate colors.")]
    public Color negativeColor;

    int phase = 0;
    bool inPhase = false;

    [Tooltip("How long sentences should show for")]
    public float lifetime = 5;
    bool runLifetime = false;

    public Comment[] allComments;

    public void Awake()
    {
        //this makes sure you start the minigame at max health
        slider.maxValue = health;
        slider.value = health;

        //empty all user comment buttons for a clean start
        int childCount = viewerComments.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            viewerComments.transform.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //access all comments
        allComments = Resources.LoadAll<Comment>("Comments"); //the "" content must match the folder within the resources folder
    }

    public void Update()
    {
        if (!inPhase) {
            StartPhase();
        }

        if (lifetime <= 0) {
            EndPhase();
        }

        if (runLifetime) {
            lifetime -= 1 * Time.deltaTime;
        }
    }
    public void StartPhase() {
        //fill yellows comments with scriptable objects
        int childCount = yellowsComments.transform.childCount;
        Debug.Log("yellowComments has " + childCount + " children");
        for (int i = 0; i < childCount+1; i++)//trick question: children counting start from 1. allComment[] starts from 0, hence the 'childCount+1'
        {
            
            //find the comment we need
            for (int j = 0; j < allComments.Length; j++) {
                if (allComments[j].name.Contains("Yellow") && allComments[j].name.StartsWith(phase + "." + i)) {
                    
                    //set text
                    yellowsComments.transform.GetChild(i-1).transform.GetComponentInChildren<TextMeshProUGUI>().text = allComments[j].Text;

                    //now color it
                    if (allComments[j].nature == Comment.Nature.Bad || allComments[j].nature == Comment.Nature.Ruminate) {
                        yellowsComments.transform.GetChild(i - 1).transform.GetComponentInChildren<TextMeshProUGUI>().color = negativeColor;
                    }
                    
                    if (allComments[j].nature == Comment.Nature.Good || allComments[j].nature == Comment.Nature.Counter)
                    {
                        yellowsComments.transform.GetChild(i - 1).transform.GetComponentInChildren<TextMeshProUGUI>().color = positiveColor;
                    }

                    Debug.Log("child " + i + " is " + yellowsComments.transform.GetChild(i - 1).name);
                }
            }          
        }

        //add viewer comments with scriptable objects. push up (push back?) previous if max length has been reached
        inPhase = true; //let script know we are in a phase
        runLifetime = true;
    }

    public void EndPhase() {
        //wipe yellows comments
        runLifetime = false;
        inPhase = false;
    }
    public void CalcHP(int value) { //recalculate health using outcome of a comment
        health += value;
        Debug.Log("Lost/gained " + value + " health. Current health: " + health);
        //update the health bar accordingly
        SetHeath();
    }

    void SetHeath() {
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
