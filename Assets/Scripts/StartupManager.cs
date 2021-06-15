using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on the parent (canvas) holding the ui elements for the startup screen
public class StartupManager : MonoBehaviour
{
    public GameObject player;
    public AudioClip mainTheme;

    private void Update()
    {
        if (Input.anyKey) {
            CloseTitle();
        }
    }

    void CloseTitle() {
        //change music to main theme
        player.GetComponent<AudioSource>().clip = mainTheme;
        player.GetComponent<AudioSource>().loop = true;


        //initiate fade out anim
        GetComponent<Animator>().SetTrigger("fade");
    }
}
