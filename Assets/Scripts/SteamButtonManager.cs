using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamButtonManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MinigameManager>(); //reference so you can steal all info in that script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ban(GameObject gameObject)
    {
        //ban the bad comment
        
    }
}
