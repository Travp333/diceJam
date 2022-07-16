using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//just handles the animatior
public class fellaAnimController : MonoBehaviour
{
    [SerializeField]
    float throwinDieOffset = .5f;
    diceCheckpoint check;
    Hand hand;
    Animator anim;  
    // Start is called before the first frame update
    public void clearDie(){
        hand.clearDie();
    }
    void Start()
    {
        check = GetComponent<diceCheckpoint>();
        anim = GetComponent<Animator>();
        hand = transform.GetChild(2).GetComponent<Hand>();
    }
    public void setHappy(){
        anim.SetBool("happy", true);
    }
    public void resetHappy(){
        anim.SetBool("happy", false);
    }
    public void setDead(){
        anim.SetBool("dead", true);
    }
    public void setHurt(){
        anim.SetBool("hurt", true);
    }
    public void resetHurt(){
        anim.SetBool("hurt", false);
    }

    public bool getHappy(){
        return anim.GetBool("happy");
    }
    public void setthrowinDie(){
        if(!getHappy()){
            anim.SetBool("throwinDie", true);
            Invoke("resetthrowinDie", throwinDieOffset);
        }
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
        if(check.stunned){
            anim.SetBool("stunned", true);
            anim.SetBool("happy", false);
        }
        else{
            anim.SetBool("stunned", false);
        }
        if(getHappy()){
            anim.SetBool("throwinDie", false);
        }
    }
}
