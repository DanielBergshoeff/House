using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// u were busy making the material of cam bg scroll


public class TextureScrolling : MonoBehaviour
{

    public Camera activeCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(activeCamera.transform);
       
    }

}
