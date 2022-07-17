using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//stores the player's stats
public class playerStats : MonoBehaviour
{
    public bool stunned;
    public int hp;
    //coins?!?!? in my video game???
    public int coins;
    public int damage;
    public int diceAmount;
    public int startingDiceAmount;
    public float stunnedcooldown;

    public void setStunned(){
        stunned = true;
        Invoke("resetStunned", stunnedcooldown);
    }
    void resetStunned(){
        stunned = false;
    }
}
