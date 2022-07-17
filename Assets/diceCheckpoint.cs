using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//acts as the enemy stats script as well as the script that assigns and checks the challenges
public class diceCheckpoint : MonoBehaviour
{
    battleController battle;
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
    int defaulthp;
    //amount of die the enemy has
    [SerializeField]
    public int diceCount = 3;
    //gate that blocks the checkpoint from double counting player dice 
    public bool gate = true;
    //gate that blocks the checkpoint from double counting enemy dice
    public bool gate2 = true;
    //refrence to instance of hand script on player
    Hand hand;
    // refrence to instance of hand script on enemy
    Hand hand2;
    int diceCountTemp;
    // bools that determine which type of checkpoint this is
    [SerializeField]
    public bool evenCheck, oddCheck, oneCheck, twoCheck, threeCheck, fourCheck, fiveCheck, sixCheck;
    // ints that determine how many dice you are looking for
    [SerializeField]
    public int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount;
    movement move;
    //stuns the enemy, stopping them from rolling any die

    public void resetHP(){
        hp = 100;
    }
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
        diceCountTemp = diceCount;
        defaulthp = hp;
        hand2 = transform.GetChild(2).GetComponent<Hand>();
        anim = this.gameObject.GetComponent<fellaAnimController>();
        
        //assigns all the necessary references
        foreach (GameObject g in FindObjectsOfType<GameObject>()){
            if (g.GetComponent<Hand>() != null && g.GetComponent<Hand>().isPlayer){
                hand = g.GetComponent<Hand>();
            }
            if (g.GetComponent<battleController>() != null ){
                battle = g.GetComponent<battleController>();
            }
            if(g.GetComponent<playerStats>() != null){
                stats = g.GetComponent<playerStats>();
                move = stats.gameObject.GetComponent<movement>();
            }
            
            if (stats != null)
            {
                RandomizeCheckpointValues(Mathf.Min(stats.diceAmount, diceCount));
            }
            }
    }
    //called by the damage taken animation so that the damage is deducted in sync with that. damage taken animation may play late as it cant let it interrupt the rolldie anim
    public void damageDeduct(){
        Debug.Log("You Dealt " + stats.damage + " damage!");
        hp -= stats.damage;
        setStunned();
    }
    public void endBattle(){
        battle.endBattle(); 
    }
    //called when the criterea of this checkpoint is met, so either the player dealt damage to the mps, or vice versa. 
    void dealDamage(bool isPlayer){
        
        if(isPlayer){
            closeGate();
            if(hp>0 && (hp - stats.damage) > 0 && diceCount > 0){ 
                //he still has hp, hurt him
                anim.setHurt();
                diceCount -= 1;
                stats.diceAmount += 1;
            }
            else if(hp <=0 || (hp - stats.damage) <=0 || diceCount <= 0){
                hp = 0;
                // he got no hp, he dead
                anim.setDead();
                hand.Success(); 
                hand2.clearDie();
                diceCount = diceCountTemp;

                
            }
        }
        else if(!isPlayer){
            closeGate2();
            if ((stats.hp - damage) > 0 && stats.diceAmount > 0){
                Debug.Log("Enemy Dealt " + damage + " damage!");
                stats.hp -= damage;
                stats.setStunned();
                anim.setHappy();
                stats.diceAmount -= 1;
                diceCount += 1;
            }
            else if (stats.hp <=0 || (stats.hp - damage) <=0 || stats.diceAmount <= 0){
                battle.endBattle();
                hand2.Success();
                Debug.Log("YOU DIED");
                move.backToStart();
                stats.hp = 100;
                stats.diceAmount = stats.startingDiceAmount;


            }
            
        }
        RandomizeCheckpointValues(Mathf.Min(stats.diceAmount, diceCount));
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
    void RandomizeCheckpointValues(int maxdice) {
        int i = Random.Range(1, 8);
        // lowering this for the sake of testing 
        int m = Random.Range(2, maxdice + 1);
        ResetCheckpointValues();
        switch (i) {
            case 1:
                oneCheck = true;
                oneCount = m;
                break;
            case 2:
                twoCheck = true;
                twoCount = m;
                break;
            case 3:
                threeCheck = true;
                threeCount = m;
                break;
            case 4:
                fourCheck = true;
                fourCount = m;
                break;
            case 5:
                fiveCheck = true;
                fiveCount = m;
                break;
            case 6:
                sixCheck = true;
                sixCount = m;
                break;
            case 7:
                evenCheck = true;
                evenCount = m;
                break;
            case 8:
                oddCheck = true;
                oddCount = m;
                break;
        }
        Debug.Log("randomized values");

    }
    void ResetCheckpointValues() {
        evenCheck = oddCheck = oneCheck = twoCheck = threeCheck = fourCheck = fiveCheck = sixCheck =false;
        evenCount = oddCount = oneCount = twoCount = threeCount = fourCount= fiveCount= sixCount = 0;
    }
}
