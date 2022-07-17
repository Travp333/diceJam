using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//spawns the dice and deals with dice rolling
public class diceRoll : MonoBehaviour
{
    
    [SerializeField]
    float[] rollSpeedRange;
    Rigidbody body;
    public bool gate = true;
    [SerializeField]
    bool playerDice = true;
    public int currentFace = 0;
    public bool stoppedMoving = false;
    public bool wasCounted = false;
    public bool wasClicked = false;
    [SerializeField]
    float nudgeStrength;
    [SerializeField]
    float nudgeRadius;

	private void OnMouseDown()
	{
        
        if (stoppedMoving) {
            wasClicked = true;
            Debug.Log("a stationary dice was clicked");

        }
	}
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
        if(stoppedMoving && body.isKinematic == false){
            body.isKinematic = true;
        }
        //commented this out in exchange for a re roll mechanic
        //if(gate && playerDice){
            //if(Input.GetKeyDown("space")){
                //body.AddExplosionForce(nudgeStrength, this.transform.position, nudgeRadius, 0, ForceMode.Impulse);
                //body.AddForce(0, Random.Range(getRandomNum(), getRandomNum()), 0, ForceMode.Impulse);
                //body.AddTorque(getRandomNum()*1f, getRandomNum()*1f, getRandomNum()*1f);
                //closeGate();
            //}
        //}
        if (body.IsSleeping())
        {
            currentFace = SetDiceValue();
            stoppedMoving = true;
        }
        else if(body.IsSleeping() && currentFace == 0){
            Debug.Log("Dice Stuck!");
            body.AddExplosionForce(nudgeStrength, this.transform.position, nudgeRadius, 0, ForceMode.Impulse);
            body.AddTorque(getRandomNum()*1f, getRandomNum()*1f, getRandomNum()*1f);
        }
        else {
            currentFace = 0;
        }
        
        
    }

    int SetDiceValue() {
        int value = 0;
        Transform t = this.transform;
        Vector3 up = t.up.normalized;
        Vector3 right = t.right.normalized;
        Vector3 forward = t.forward;
        
        if (Vector3.Angle(up, Vector3.up) < 10f) {
            value = 6;
        }
        if (Vector3.Angle(-up, Vector3.up) < 10f)
        {
            value = 1;
        }
        if (Vector3.Angle(right, Vector3.up) < 10f)
        {
            value = 4;
        }
        if (Vector3.Angle(-right, Vector3.up) < 10f)
        {
            value = 3;
        }
        if (Vector3.Angle(forward, Vector3.up) < 10f)
        {
            value = 5;
        }
        if (Vector3.Angle(-forward, Vector3.up) < 10f)
        {
            value = 2;
        }
        return value;
    }

}
