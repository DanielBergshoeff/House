using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

//place this script on the ui element that must sit on its parent (NPC character) so that players can click on them to talk
//based off of https://www.youtube.com/watch?v=tHEG95vrO_Q

public class ClickToTalk : MonoBehaviour
{


    public void startTalking()
    {
        GetComponentInChildren<Flowchart>().StopAllBlocks(); //make sure the same flowchart cannot happen twice
        Destroy(GameObject.Find("MenuDialog")); //destroy the menu in hierarchy to prevent the menus from staying in view when walking out of the talk zone of this scripts parent
    }
}
