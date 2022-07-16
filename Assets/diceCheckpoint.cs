using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceCheckpoint : MonoBehaviour
{
    public int hp = 100;
    bool gate = true;
    Hand hand;
    Hand hand2;
    [SerializeField]
    bool evenCheck, oddCheck, oneCheck, twoCheck, threeCheck, fourCheck, fiveCheck, sixCheck;
    [SerializeField]
    int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount;
    // Start is called before the first frame update
    public void openGate(){
        gate = true;
    }
    void closeGate(){
        gate = false;
    }
    void Start()
    {
        foreach (GameObject g in FindObjectsOfType<GameObject>()){
            if (g.GetComponent<Hand>() != null && g.GetComponent<Hand>().isPlayer){
                hand = g.GetComponent<Hand>();
            }
            else if (g.GetComponent<Hand>() != null && !g.GetComponent<Hand>().isPlayer){
                hand2 = g.GetComponent<Hand>();
            }
        }
    }

    void dealDamage(){
        closeGate();
        if(hp>0){
            hp -= hand.damage;
        }
        else{
            hand.Success(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gate){
            if(evenCheck){
                if(hand.evenCount >= evenCount){
                    dealDamage();    
                }
                else if(hand2.evenCount >= evenCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if (oddCheck){
                if(hand.oddCount >= oddCount){
                    dealDamage(); 
                }
                else if(hand2.oddCount >= oddCount){
                    hand2.Success();
                    closeGate();
                }

            }
            if (oneCheck){
                if(hand.oneCount >= oneCount){
                    dealDamage(); 
                }
                else if(hand2.oneCount >= oneCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if(twoCheck){
                if(hand.twoCount >= twoCount){
                    dealDamage(); 
                }
                else if(hand2.twoCount >= twoCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if(threeCheck){
                if(hand.threeCount >= threeCount){
                    dealDamage(); 
                }
                else if(hand2.threeCount >= threeCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if(fourCheck){
                if(hand.fourCount >= fourCount){
                    dealDamage(); 
                }
                else if(hand2.fourCount >= fourCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if(fiveCheck){
                if(hand.fiveCount >= fiveCount){
                    dealDamage(); 
                }
                else if(hand2.evenCount >= evenCount){
                    hand2.Success();
                    closeGate();
                }
            }
            else if(sixCheck){
                if(hand.sixCount >= sixCount){
                    dealDamage(); 
                }
                else if(hand2.sixCount >= sixCount){
                    hand2.Success();
                    closeGate();
                }
            }




        }
    }
}
