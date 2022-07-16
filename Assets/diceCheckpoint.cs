using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceCheckpoint : MonoBehaviour
{
    bool gate = true;
    Hand hand;
    [SerializeField]
    bool evenCheck, oddCheck, oneCheck, twoCheck, threeCheck, fourCheck, fiveCheck, sixCheck;
    [SerializeField]
    int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount;
    // Start is called before the first frame update
    void openGate(){
        gate = true;
    }
    void Defeated(){
        gate = false;
    }
    void Start()
    {
        foreach (GameObject g in FindObjectsOfType<GameObject>()){
            if (g.GetComponent<Hand>() != null){
                hand = g.GetComponent<Hand>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gate){
            if(evenCheck){
                if(hand.evenCount >= evenCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if (oddCheck){
                if(hand.oddCount >= oddCount){
                    hand.Success();
                    Defeated();
                }
            }
            if (oneCheck){
                if(hand.oneCount >= oneCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if(twoCheck){
                if(hand.twoCount >= twoCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if(threeCheck){
                if(hand.threeCount >= threeCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if(fourCheck){
                if(hand.fourCount >= fourCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if(fiveCheck){
                if(hand.fiveCount >= fiveCount){
                    hand.Success();
                    Defeated();
                }
            }
            else if(sixCheck){
                if(hand.sixCount >= sixCount){
                    hand.Success();
                    Defeated();
                }
            }
        }
    }
}
