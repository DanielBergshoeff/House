using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    public GameObject go1;
    public GameObject go2;

    // 0 = closed
    // 1 = open to go1
    // 2 = open to go2
    public int CurrentState = 0;

    public Animator MyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = GetComponentInChildren<Animator>();
        go1.GetComponent<CurtainPart>().MyCurtain = this;
        go2.GetComponent<CurtainPart>().MyCurtain = this;
    }
}
