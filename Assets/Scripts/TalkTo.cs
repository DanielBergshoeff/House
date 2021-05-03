using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//put this script on any NPC or object through which you want to use fungus dialogue trees (through prefabs plz)

public class TalkTo : MonoBehaviour
{
    public InputActionAsset playerControls;
    bool isTalking = false; //check if youre talking or not

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
                Debug.Log("Collided with player");
            if (isTalking == false && Input.GetKeyDown("e"))
            {
                isTalking = true;
                Debug.Log("Pressed something");
            }
        }
    }


}
