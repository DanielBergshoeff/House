using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    public Animator animator;
    public BoxCollider otherCollider;
    [Tooltip("type which local rotation the door must open; -x or x.")]
    public string direction;

    void OnTriggerEnter(Collider other)
    {
        //if u touch player
        if (other.gameObject.CompareTag("Player")) {
            animator.SetTrigger(direction);
        }        
    }
    
}
