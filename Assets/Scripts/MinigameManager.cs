using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    [Header("Comment containers")]
    public GameObject button1;
    public GameObject button2;
    [Tooltip("Insert whatever parent directly holds the 2 game objects that are Yellow's sentences.")]
    public GameObject yellowsComments;
    [Tooltip("Insert the parent that holds all the viewers comments.")]
    public GameObject viewerComments;
    public GameObject viewersCommentCover;
    
    public Slider slider;
    public int health = 10;

    [Header("Comment values")]
    public int badCommentValue = 1; //for some reason, negative numbers become positive when bounced between scripts. The - is now thrown in the StreamComments script.
    public int goodCommentValue = 1;
    public int ruminateValue = 2;
    public int counterValue = 2;
    [Tooltip("Set color for good and counter colors.")]
    public Color positiveColor;
    [Tooltip("Set color for bad and ruminate colors.")]
    public Color negativeColor;
    public Color neutralColor;

    [Header("Minigame values")]
    public int phase = 0;
    public int phaseAmount;
    public bool inPhase = false;
    private int _phasePoints = 0;

    [Tooltip("How long sentences should show for")]
    public float lifetime = 5;
    public float remainingLifetime;
    bool runLifetime = false;

    public Comment[] allComments;

    public void Awake()
    {
        //this makes sure you start the minigame at max health
        slider.maxValue = health;
        slider.value = health;

        remainingLifetime = lifetime;

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
        //have all comments show themselves
        yellowsComments.GetComponent<Animator>().SetBool("slideIn", true);
        viewersCommentCover.GetComponent<Animator>().SetBool("reveal", true);
        
        remainingLifetime = lifetime;
        //button1.GetComponent<StreamButton>().GetComponentInChildren<StreamButton>().EmptyComment();

        //find comment for button 1
        for (int i = 0; i < allComments.Length; i++)
        {
            if (allComments[i].speaker == Comment.Speaker.Yellow && allComments[i].name.StartsWith(phase + "." + 1))
            {
                button1.GetComponent<StreamButton>().SetComment(allComments[i]);
                Debug.Log("marker1.1");
            }
        }

        //find comment for button 2
        for (int i = 0; i < allComments.Length; i++)
        {
            if (allComments[i].speaker == Comment.Speaker.Yellow && allComments[i].name.StartsWith(phase + "." + 2))
            {
                button2.GetComponent<StreamButton>().SetComment(allComments[i]);
                Debug.Log("marker2.1");
            }
        }

        //add viewer comments with scriptable objects. push up (push back?) previous if max length has been reached
        int childCountB = viewerComments.transform.childCount;
        for (int i = 0; i < childCountB + 1; i++)
        {
            //find the comment we need
            for (int j = 0; j < allComments.Length; j++)
            {
                if (allComments[j].name.Contains("Viewer") && allComments[j].name.StartsWith(phase + "." + i))
                {

                    //set text
                    viewerComments.transform.GetChild(i - 1).transform.GetComponentInChildren<TextMeshProUGUI>().text = "user: " + allComments[j].Text;
                }
            }
        }

        inPhase = true; //let script know we are in a phase
        runLifetime = true; //allow the starting of the remainingLifetime counter
        //looking for a yellows or viewers comments follows the same algorithm. Which means there is room for optimization.
    }

    public void EndPhase() {

        //wipe all yellows comment buttons. Viewer comments are simply refilled with emptyComment.
        //button1.GetComponent<StreamButton>().EmptyComment(); //wipe button 1
        //button2.GetComponent<StreamButton>().EmptyComment(); //wipe button 2

        //wipe all user comment buttons
        int childCountB = viewerComments.transform.childCount;
        for (int i = 0; i < childCountB; i++)
        {
            viewerComments.transform.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        
        //have Yellows comments animation disabled, so it will play next time it gets enabled, holding new text.
        yellowsComments.GetComponent<Animator>().SetBool("slideIn", false);
        viewersCommentCover.GetComponent<Animator>().SetBool("reveal", false);

        runLifetime = false;
        CalcHP();
        inPhase = false;
        phase += 1;
    }

    public void CalcHP() { //recalculate health using outcome of a comment
        health += _phasePoints;
        Debug.Log("Lost/gained " + _phasePoints + " health. Current health: " + health);
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
