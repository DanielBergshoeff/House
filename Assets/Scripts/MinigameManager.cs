using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    [Tooltip("Drag and drop the parent object of all stream menu elements.")]
    public GameObject menuCanvas;
    public StreamAudioManager audioManager;

    [Header("Comment containers")]
    [Tooltip("Insert whatever parent directly holds the 2 game objects that are Yellow's sentences.")]
    public GameObject yellowsComments;
    [Tooltip("Insert the parent that holds all the viewers comments.")]
    public GameObject viewerComments;
    public GameObject viewersCommentCover;
    [Tooltip("An empty default comment this script uses to wipe other comments with its mostly neutral attributes. Stream button script uses the same scriptable object for different means. Can be located under Prefabs > Sentences.")]
    public Comment emptyComment;
    
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
    public float lerpModefier = 0.5f;
    public GameObject goodCommentClickEffect;

    [Header("Minigame values")]
    public int phase = 0;
    public int phaseAmount;
    public bool inPhase = false;

    [Tooltip("How long sentences should show for")]
    public float lifetime = 5;
    public float remainingLifetime;
    bool runLifetime = false;

    [Header("Tooltip settings")]
    public Canvas tooltipCanvas;
    public GameObject tooltip;

    [Header("Dirty hacky code")]
    public GameObject yellowChild1;
    public GameObject yellowChild2;

    public Comment[] allComments;

    public void Awake()
    {
        emptyComment.interactedWith = false;//make sure they are all 'untouched'

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
        //start animations
        yellowsComments.GetComponent<Animator>().SetBool("slideIn", true);
        viewersCommentCover.GetComponent<Animator>().SetBool("reveal", true);
        
        remainingLifetime = lifetime;


        //fill yellows comments with scriptable objects
        for (int i = 0; i < allComments.Length; i++)
        {
            if (allComments[i].name.Contains("Yellow") && allComments[i].name.StartsWith(phase + "." + 1))
            { //find the comment for the 1st button, hence the 1
                yellowChild1.GetComponentInChildren<StreamButton>().buttonComment = allComments[i]; //pass on the correct comment
            }
            if (allComments[i].name.Contains("Yellow") && allComments[i].name.StartsWith(phase + "." + 2))
            { //find the comment for the 2nd button, hence the 2
                yellowChild2.GetComponentInChildren<StreamButton>().buttonComment = allComments[i]; //pass on the correct comment
            }
        }

        //add viewer comments with scriptable objects
        int childCountB = viewerComments.transform.childCount;
        for (int i = 0; i < childCountB + 1; i++)
        {
            //find the comment we need
            for (int j = 0; j < allComments.Length; j++)
            {
                if (allComments[j].name.Contains("Viewer") && allComments[j].name.StartsWith(phase + "." + i))
                {
                    //set buttonComment
                    viewerComments.transform.GetChild(i - 1).GetComponent<StreamButton>().buttonComment = allComments[j];
                }
            }
        }

        inPhase = true; //let script know we are in a phase
        runLifetime = true; //allow the starting of the remainingLifetime counter
    }

    public void EndPhase() {
        //reset _phasePoints if you wanna use that
        gameObject.GetComponent<StreamHealth>().CalcHP(); //calculate all negative and positive numbers

        //wipe all viewer comment buttons
        int childCountB = viewerComments.transform.childCount;
        for (int i = 0; i < childCountB; i++) {
            viewerComments.transform.GetChild(i).GetComponent<StreamButton>().buttonComment = emptyComment;
        }
        //wipe all Yellow comment buttons
        int childCountA = yellowsComments.transform.childCount;
        for (int i = 0; i < childCountA; i++) {
            yellowsComments.transform.GetChild(i).GetComponent<StreamButton>().buttonComment = emptyComment;
        }

       
        //have Yellows comments animation disabled, so it will play next time it gets enabled, holding new text.
        yellowsComments.GetComponent<Animator>().SetBool("slideIn", false);
        viewersCommentCover.GetComponent<Animator>().SetBool("reveal", false);
        
        runLifetime = false;
        inPhase = false;
        phase += 1;
    }



    void EndMinigame() {
        yellowsComments.SetActive(false);
        viewerComments.SetActive(false);
        menuCanvas.GetComponent<StreamMenuProtocol>().FinishProtocol(); //initiate the finish state of the minigame by calling the right function
    }
}
