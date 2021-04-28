using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;//well this was prod and poked 7:28

//code used from samyam at: https://www.youtube.com/watch?v=H2RLv0aWUB0

//InputSystemUIInputModule inputModule;
public class Tooltip : MonoBehaviour
{
    //InputSystemUIInputModule inputModule;
    // Update is called once per frame
       
    //reference your action map so u can talk about their items
    private PlayerInput controls;


    private void Awake() {
        controls = new PlayerInput(); //this instantiates controls apparently
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    void Update()
    {
    //vector2 mouseposition = controls.player
    //vector2 position = input.mouseposition;
    //transform.position = position; //then set the pos of tooltip base off mouse pos
    }
}
