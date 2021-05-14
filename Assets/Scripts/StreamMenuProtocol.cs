using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//do we need this?
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//How to load scene:https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html

//this script sits on the parent that holds all stream menu elements

public class StreamMenuProtocol : MonoBehaviour
{
    [Header("References")]
    public GameObject minigameManager;
    //reference to all important components
    //title text
    public GameObject titleText;
    //Button
    public GameObject button;
    //menu BG
    public GameObject menuBG; //this must be the gameobj, NOT an image component for we want to use this as a shield preventing the player from clicking anything behind it (buttons and the like)

    public enum protocol { Start, Play, Skip, Finish };
    [Header("Testing purposes only")]
    public protocol currentProtocol;


    private void Awake()
    {
        StartProtocol();
    }
    public void StartProtocol()
    {
        //enable all hidden elements
        menuBG.SetActive(true);
        titleText.SetActive(true);
        button.SetActive(true);

        //set text to Ready to go live?
        titleText.GetComponent<TextMeshProUGUI>().text = "Ready to go live?";
        //display the button + set button text
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Start!";
        //set the right action for that button
    }

    public void clickedIt()
    {
        //no lets allow it to do what needs to be done depending on the current protocol.
        if (currentProtocol == protocol.Start)
        {
            currentProtocol = protocol.Play; //change from the current protocol to the new one
            PlayProtocol(); //initiate the functionalities of the next protocol
        }
        if (currentProtocol == protocol.Finish)
        {
            //load overworld scene
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene()); //copied this from unity. View link at the top for more info
        }
    }

    public void PlayProtocol()
    {
        //hide all this stuff
        menuBG.SetActive(false);
        titleText.SetActive(false);
        button.SetActive(false);
        minigameManager.GetComponent<MinigameManager>().enabled = true;
    }

    public void FinishProtocol()
    {
        currentProtocol = protocol.Finish;
        minigameManager.GetComponent<MinigameManager>().enabled = false; //disable minigame manager so it wont keep on running

        //enable all elements so you can see it and shield the interactables begind the menu BG
        menuBG.SetActive(true);
        titleText.SetActive(true);
        button.SetActive(true);

        //set text
        titleText.GetComponent<TextMeshProUGUI>().text = "End of the stream!";
        //display the button + set button text
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log out";
    }

    public void SkipProtocol() {
        currentProtocol = protocol.Skip;
        menuBG.SetActive(true);//to make text readable and hide animations in the back.
        
        titleText.GetComponent<TextMeshProUGUI>().text = "A few moments later...";
        titleText.SetActive(true);
    }

    IEnumerator LoadYourAsyncScene()//copied this from unity. View link at the top for more info
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Bedroom");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
