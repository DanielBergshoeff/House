using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChairSurfing : MonoBehaviour
{
    public CinemachineVirtualCamera oldcam;
    public CinemachineVirtualCamera newcam;

    // Update is called once per frame
    void Start()
    {
        SwitchCam();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //CM camera switch
        SwitchCam();
        //players cannot detatch from chair
        //players control chair on chair
        //get new controls?
    }

    public void SwitchCam() {
        //set active CM cam from one to the other
        //or move transforms?
        Debug.Log("initiating SwitchCam()");
        oldcam.transform.position = new Vector3(999, 999, 999);


    }
}
