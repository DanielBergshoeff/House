using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on the parent (canvas) holding the ui elements for the startup screen
public class StartupManager : MonoBehaviour
{
    public GameObject mainAudioPlayer;
    public AudioClip mainTheme;

    private AudioSource _anim;

    private void Awake()
    {
        _anim = mainAudioPlayer.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //make sure the game only starts playing when player uses a key. we check if an animation isnt already playing, else youd interrup whatever sound mainAudioPlayer is playing for main Theme
        if (Input.anyKey && _anim.isPlaying == false) {
            CloseTitle();
        }
    }

    void CloseTitle() {
        //change music to main theme
        _anim.clip = mainTheme;
        _anim.loop = true;
        _anim.Play();


        //initiate fade out anim
        GetComponent<Animator>().SetTrigger("fade");
        GetComponent<StartupManager>().enabled = false; //shut this off so script wont keep doing update
    }
}
