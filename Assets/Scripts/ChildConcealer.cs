using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reveal the childed objects in a fade when player enters the collider

public class ChildConcealer : MonoBehaviour
{
    private int _amountOfChildren;
    
    private void OnTriggerEnter(Collider other)
    {
        //check if its the player
        if (other.CompareTag("Player"))
        {
            RevealChildren();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //check if its the player
        if (other.CompareTag("Player"))
        {
            HideChildren();
        }
    }

    void RevealChildren()
    {
        CountChildren();
        //use that number to cycle through and affect children
        for (int i = 0; i < _amountOfChildren; i++)
        {transform.GetChild(i).gameObject.SetActive(true);}//enable
    }

    void HideChildren() {
        CountChildren();
        for (int i = 0; i < _amountOfChildren; i++)
        {transform.GetChild(i).gameObject.SetActive(false);}//disable
    }

    void CountChildren() {
        //learn amount of children on this Parent
        _amountOfChildren = transform.childCount;}
}
