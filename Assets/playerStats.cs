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
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("FUCK");
    }
}
