using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fellaAnimController : MonoBehaviour
{
    Hand hand;
    Animator anim;  
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hand = transform.GetChild(2).GetComponent<Hand>();
    }

    public void setthrowinDie(){
        anim.SetBool("throwinDie", true);
        Invoke("resetthrowinDie", .5f);
    }
    void resetthrowinDie(){
        anim.SetBool("throwinDie", false);
    }

    public void reRoll(){
        hand.reRoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
