using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is where I define the amount of penalty points or HP there are.

public class MinigameManager : MonoBehaviour
{
    public Slider slider;

    public int health = 10;
    public float badCommentValue = 1f;
    public float goodCommentValue = 1f;
    public float ruminateValue = 2;
    public float counterValue = 2;

    public void Awake() {
        //this makes sure you start the minigame at max health
        slider.maxValue = health;
        slider.value = health;
    }

    public void CalcHP(int value) { //recalculate health using outcome of a comment
        health += value;
        Debug.Log("Lost/gained " + value + " health.");
        //update the health bar accordingly
        SetHeath();
    }

    void SetHeath() {
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
