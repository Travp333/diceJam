using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoll : MonoBehaviour
{
    [SerializeField]
    float[] rollSpeedRange;
    Rigidbody body;
    public bool gate = true;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    float getRandomNum(){
        return Random.Range(rollSpeedRange[0], rollSpeedRange[1]);
    }

    public void openGate(){
        gate = true;
    }
    void closeGate(){
        gate = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground"){
            openGate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gate){
            if(Input.GetKeyDown("space")){
                body.AddForce(0, Random.Range(getRandomNum(), getRandomNum()), 0, ForceMode.Impulse);
                body.AddTorque(getRandomNum()*50f, getRandomNum()*50f, getRandomNum()*50f);
                closeGate();
            }
        }
        
    }
}
