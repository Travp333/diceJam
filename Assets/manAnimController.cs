using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manAnimController : MonoBehaviour
{
    Animator anim;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }
    public void setMoving(bool plug){
        anim.SetBool("moving", plug);
    }   
}
