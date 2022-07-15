using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceReset : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<diceRoll>() != null){
            other.gameObject.GetComponent<diceRoll>().openGate();
        }
    }
}
