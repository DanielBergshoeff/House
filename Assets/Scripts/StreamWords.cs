using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //do we need this? (optimizse)

public class StreamWords : MonoBehaviour
{
    //be good, bad, or neutral
    public bool isNeutral = true;
    public bool isBad = false;
    public bool isGood = false;

    private void Start()
    {
        //color em
        if (isBad)
        { GetComponentInChildren<TextMeshProUGUI>().color = Color.red; }
        if (isGood)
        { GetComponentInChildren<TextMeshProUGUI>().color = Color.green; }
    }
    public void Ban() {
        if (isBad) {
         Debug.Log(GetComponentInChildren<TextMeshProUGUI>().text);//print the text in debug before destruction
        //play neutralize visual()
        Destroy(gameObject);
        }
    }
    public void Highlight() {
        if (isGood) {
            //add points
            //play highlight visual
        }
    }
    public void Neutral() { 
        //play neutral visual()
    }
    public void Ruminate() { 
        //subtract points
        //play ruminate visual
    }
    public void Counter() {
        //add points
        //play counter visual
    }
}
