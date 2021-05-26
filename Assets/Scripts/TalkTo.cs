using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

//put this script on any NPC or object through which you want to use fungus dialogue trees (through prefabs plz)

public class TalkTo : MonoBehaviour
{
    public InputActionAsset playerControls;
    bool isTalking = false; //check if youre talking or not
    public bool useTriggerCollider = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && useTriggerCollider == true)
        {
            Debug.Log("Collided with player");

            GetComponentInChildren<Flowchart>().ExecuteBlock("Start");
            //if (isTalking == false && Input.GetKeyDown("e"))
            //{
            //    isTalking = true;
            //    Debug.Log("Pressed something");
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            GetComponentInChildren<Flowchart>().StopAllBlocks(); //make sure the same flowchart cannot happen twice
            Destroy(GameObject.Find("MenuDialog")); //destroy the menu in hierarchy to prevent the menus from staying in view when walking out of the talk zone of this scripts parent
        }
        Debug.Log("Exited NPC talk trigger collider");
    }

    public void startTalking() {
        GetComponentInChildren<Flowchart>().StopAllBlocks(); //make sure the same flowchart cannot happen twice
        Destroy(GameObject.Find("MenuDialog")); //destroy the menu in hierarchy to prevent the menus from staying in view when walking out of the talk zone of this scripts parent
    }

}
