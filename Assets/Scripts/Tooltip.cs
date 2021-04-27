using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InputSystemUIInputModule inputModule;
public class Tooltip : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 position = Input.mousePosition;
        transform.position = position; //then set the pos of tooltip base off mouse pos
    }
}
