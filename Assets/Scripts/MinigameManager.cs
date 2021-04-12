using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is where I define the amount of penalty points or HP there are.

public class MinigameManager : MonoBehaviour
{
    [Tooltip("Insert whatever parent directly holds the 2 game objects that are Yellow's sentences")]
    public GameObject sentencesParent;
    
    public Slider slider;

    public int health = 10;
    public int badCommentValue = 1; //for some reason, negative numbers become positive when bounced between scripts. The - is now thrown in the StreamComments script.
    public int goodCommentValue = 1;
    public int ruminateValue = 2;
    public int counterValue = 2;
    [Tooltip("How long sentences should show for")]
    public float lifetime = 5;

    public void Awake() {
        //this makes sure you start the minigame at max health
        slider.maxValue = health;
        slider.value = health;
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






    //public void StartDialogue() => StartDialogue(startNode);
}
