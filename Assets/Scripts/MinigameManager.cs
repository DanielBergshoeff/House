﻿using System.Collections;
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
    public Color neutralColor;

    public int phase = 0;
    public int phaseAmount;
    public bool inPhase = false;

    [Tooltip("How long sentences should show for")]
    public float lifetime = 5;
    public float remainingLifetime;
    bool runLifetime = false;

    public Comment[] allComments;
    public float firstViewerCommentDelay = 1f;
    public float consecutiveViewerCommentDelay = 0.25f;

    public void Awake()
    {
        //this makes sure you start the minigame at max health
        slider.maxValue = health;
        slider.value = health;

        remainingLifetime = lifetime;

        //empty all user comment buttons for a clean start
        int childCountA = viewerComments.transform.childCount;
        for (int i = 0; i < childCountA; i++) {
            viewerComments.transform.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //access all comments
        allComments = Resources.LoadAll<Comment>("Comments"); //the "" content must match the folder within the resources folder
    }

    public void Update()
    {
        if (!inPhase && phase != phaseAmount + 1) {
            StartPhase();
        }

        if (remainingLifetime <= 0) {
            EndPhase(); //note the phase number gets incremented here
        }

        if (runLifetime) {
            remainingLifetime -= 1 * Time.deltaTime;
        }

        if (phase > phaseAmount) {
            EndMinigame();
        }
    }
    public void StartPhase() {
        //have Yellows comments start
        yellowsComments.GetComponent<Animator>().enabled = true;
        
        remainingLifetime = lifetime;

        //fill yellows comments with scriptable objects
        int childCountA = yellowsComments.transform.childCount;
        Debug.Log("yellowComments has " + childCountA + " children");
        for (int i = 0; i < childCountA + 1; i++)//trick question: children counting start from 1. allComment[] starts from 0, hence the 'childCountA+1'
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

                    else { yellowsComments.transform.GetChild(i - 1).transform.GetComponentInChildren<TextMeshProUGUI>().color = neutralColor; }
                }
            }          
        }

        //add viewer comments with scriptable objects. push up (push back?) previous if max length has been reached
        int childCountB = viewerComments.transform.childCount;
        Debug.Log("viewerComments has " + childCountB + " children");
        for (int i = 0; i < childCountB + 1; i++)
        {
            //find the comment we need
            for (int j = 0; j < allComments.Length; j++)
            {
                if (allComments[j].name.Contains("Viewer") && allComments[j].name.StartsWith(phase + "." + i))
                {

                    //set text
                    viewerComments.transform.GetChild(i - 1).transform.GetComponentInChildren<TextMeshProUGUI>().text = allComments[j].Text;
                }
            }
        }

        inPhase = true; //let script know we are in a phase
        runLifetime = true; //allow the starting of the remainingLifetime counter
        //looking for a yellows or viewers comments follows the same algorithm. Which means there is room for optimization.
    }

    public void EndPhase() {

        //have Yellows comments animation disabled, so it will play next time it gets enabled, holding new text.
        yellowsComments.GetComponent<Animator>().enabled = false;
        Debug.Log("Animation component state is " + yellowsComments.GetComponent<Animator>().isActiveAndEnabled);

        //wipe all yellows comment buttons. Viewer comments are simply refilled with empty "".
        int childCountA = yellowsComments.transform.childCount;
        for (int i = 0; i < childCountA; i++)
        {
            yellowsComments.transform.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //wipe all user comment buttons
        int childCountB = viewerComments.transform.childCount;
        for (int i = 0; i < childCountB; i++)
        {
            viewerComments.transform.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
   
        runLifetime = false;
        inPhase = false;
        phase += 1;
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

    void EndMinigame() {
        yellowsComments.SetActive(false);
        viewerComments.SetActive(false);
        Debug.Log("Minigame has ended!");
    }
}
