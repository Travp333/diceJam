using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//just handles the animatior
public class fellaAnimController : MonoBehaviour
{
    [SerializeField]
    bool justIdle;
    [SerializeField]
    float throwinDieOffset = .5f;
    diceCheckpoint check;
    Hand hand;
    Animator anim;  
    bool happy;
    public float Tick;
    // Start is called before the first frame update
    public void clearDie(){
        hand.clearDie();
    }
    void Awake()
    {
        Tick = 0f;
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
        Tick = 0f;
        if(!happy && anim != null){
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
        if(anim.GetBool("happy")){
            happy = true;
        }
        else{
            happy = false;
        }
        if(justIdle){
            anim.SetBool("throwinDie", false);
            anim.SetBool("stunned", false);
            anim.SetBool("happy", false);
            anim.SetBool("hurt", false);
        }
        else{
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
        if(!justIdle && !anim.GetBool("stunned") && !anim.GetBool("happy") && !anim.GetBool("throwinDie") && !anim.GetBool("hurt") && !anim.GetBool("dead") && Tick < 50){
            Tick += Time.deltaTime;
        }
        if(Tick > 50){
            Tick = 0f;
            setthrowinDie();
        }

    }
}
