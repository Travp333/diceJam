using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//acts as the enemy stats script as well as the script that assigns and checks the challenges
public class diceCheckpoint : MonoBehaviour
{
    //amount of damage the enemy does to you
    public int damage;
    //refrence to the enemies animator
    fellaAnimController anim;
    //how long the enemy stays stunned after taking damage
    [SerializeField]
    float stunnedTimer = 3f;
    //stunned status on enemy
    public bool stunned;
    //refrence to playerstats script which keeps track of all the players stats
    playerStats stats;
    //enemies HP
    public int hp = 100;
    //amount of die the enemy has
    [SerializeField]
    public int diceCount;
    //gate that blocks the checkpoint from double counting player dice 
    public bool gate = true;
    //gate that blocks the checkpoint from double counting enemy dice
    public bool gate2 = true;
    //refrence to instance of hand script on player
    Hand hand;
    // refrence to instance of hand script on enemy
    Hand hand2;
    // bools that determine which type of checkpoint this is
    [SerializeField]
    bool evenCheck, oddCheck, oneCheck, twoCheck, threeCheck, fourCheck, fiveCheck, sixCheck;
    // ints that determine how many dice you are looking for
    [SerializeField]
    int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount;
    //stuns the enemy, stopping them from rolling any die
    public void setStunned(){
        stunned = true;
        Invoke("resetStunned", stunnedTimer);
    }
    //resets the enemies stunned status
    void resetStunned(){
        stunned = false;
    }

    public void openGate(){
        gate = true;
    }
    public void openGate2(){
        gate2 = true;
    }

    void closeGate(){
        gate = false;
    }
    void closeGate2(){
        gate2 = false;
    }
    void Start()
    {
        //assigns all the necessary references
        foreach (GameObject g in FindObjectsOfType<GameObject>()){
            if (g.GetComponent<Hand>() != null && g.GetComponent<Hand>().isPlayer){
                hand = g.GetComponent<Hand>();
            }
            else if (g.GetComponent<Hand>() != null && !g.GetComponent<Hand>().isPlayer){
                hand2 = g.GetComponent<Hand>();
            }
            if(g.GetComponent<playerStats>() != null){
                stats = g.GetComponent<playerStats>();
            }
            if(g.GetComponent<fellaAnimController>()!= null){
                anim = g.GetComponent<fellaAnimController>();
            }
        }
    }
    //called by the damage taken animation so that the damage is deducted in sync with that. damage taken animation may play late as it cant let it interrupt the rolldie anim
    public void damageDeduct(){
        Debug.Log("You Dealt " + stats.damage + " damage!");
        hp -= stats.damage;
        setStunned();
    }
    //called when the criterea of this checkpoint is met, so either the player dealt damage to the mps, or vice versa. 
    void dealDamage(bool isPlayer){
        
        if(isPlayer){
            closeGate();
            if(hp>0 && (hp - stats.damage) > 0){
                //he still has hp, hurt him
                anim.setHurt();
            }
            else if(hp <=0 || (hp - stats.damage) <=0){
                hp = 0;
                // he got no hp, he dead
                anim.setDead();
                hand.Success(); 
            }
        }
        else if(!isPlayer){
            closeGate2();
            if (stats.hp > 0){
                Debug.Log("Enemy Dealt " + damage + " damage!");
                stats.hp -= damage;
                stats.setStunned();
                anim.setHappy();
            }
            else{
                hand2.Success();
            }
        }
    }

    void Update()
    {
        //checking to see if the checkpoints criterea have been met on the player's hand
        if(gate){
            //Debug.Log("Checking rolled die...");
            if(evenCheck){
                if(hand.evenCount >= evenCount){
                    dealDamage(true);    
                }
            }
            else if (oddCheck){
                if(hand.oddCount >= oddCount){
                    dealDamage(true); 
                }
            }
            if (oneCheck){
                if(hand.oneCount >= oneCount){
                    dealDamage(true); 
                }
            }
            else if(twoCheck){
                if(hand.twoCount >= twoCount){
                    dealDamage(true); 
                }
            }
            else if(threeCheck){
                if(hand.threeCount >= threeCount){
                    dealDamage(true); 
                }
            }
            else if(fourCheck){
                if(hand.fourCount >= fourCount){
                    dealDamage(true); 
                }
            }
            else if(fiveCheck){
                if(hand.fiveCount >= fiveCount){
                    dealDamage(true); 
                }
            }
            else if(sixCheck){
                if(hand.sixCount >= sixCount){
                    dealDamage(true); 
                }
            }
        }
        //checking to see if the checkpoints criterea have been met on the enemies's hand
        if(gate2){
            if(evenCheck){
                if(hand2.evenCount >= evenCount){
                    dealDamage(false);    
                }
            }
            else if (oddCheck){
                if(hand2.oddCount >= oddCount){
                    dealDamage(false); 
                }
            }
            if (oneCheck){
                if(hand2.oneCount >= oneCount){
                    dealDamage(false); 
                }
            }
            else if(twoCheck){
                if(hand2.twoCount >= twoCount){
                    dealDamage(false); 
                }
            }
            else if(threeCheck){
                if(hand2.threeCount >= threeCount){
                    dealDamage(false); 
                }
            }
            else if(fourCheck){
                if(hand2.fourCount >= fourCount){
                    dealDamage(false); 
                }
            }
            else if(fiveCheck){
                if(hand2.fiveCount >= fiveCount){
                    dealDamage(false); 
                }
            }
            else if(sixCheck){
                if(hand2.sixCount >= sixCount){
                    dealDamage(false); 
                }
            }
        }
    }
}
