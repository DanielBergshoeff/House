using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script should sit on the minigame manager alongside minigame manager script

public class StreamHealth : MonoBehaviour
{
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
        health += _phasePoints;
        //sets the value or referenced slider to current health
        slider.value = health;
    }
}
