using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this script sits on...?
public class StreamCountdown : MonoBehaviour
{
    public float secondsToCount = 3;
    public GameObject menuCanvas;
    public TextMeshProUGUI text;
    public GameObject button;

    private bool _counting = false;
    private float _remainingSeconds;

    private void Awake() {
        _remainingSeconds = secondsToCount;
    }

    private void Update()
    {
        if (_counting == true) {
            _remainingSeconds -= 1 * Time.deltaTime;
            //update the number 
            
            text.text = "Stream starting in: " + _remainingSeconds.ToString("0");//"0" makes sure it displays in full numbers. if you use "0.0" it will just show one decimal etc.
        }

        if(_remainingSeconds <= 0f) { //end the countdown
            
            gameObject.GetComponent<MinigameManager>().enabled = true; //enable the script that runs the meat of the minigame   
            //then disable canvas
            menuCanvas.SetActive(false);

            //set this inactive
        }
    }

    //wait for a button to be pressed
    public void StartCountdown() {
        button.SetActive(false);//hide button
        _counting = true;
    }
}
