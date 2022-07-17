using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
	battleController battle;
    [SerializeField]
    TextMeshProUGUI mainText = default;
	public float tickLength = .5f;
	float tickCount = 0;
    public string messageToSay;
	[SerializeField]
	movement playerMove = default;
	[SerializeField]
	diceCheckpoint ch = default;
	bool battleScene = true;


	void Start()
	{
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
            if(g.GetComponent<battleController>() != null){
                battle = g.GetComponent<battleController>();
            }
        }
	}
	
	
	private void Update()
	{
		
		if (messageToSay != null)
		{
			mainText.text = messageToSay;
		}

		if (battleScene) {
			string cond1 = null;
			string cond2;
			ReadCheckPointValues(ch, out cond1, out cond2);
			messageToSay = "You need " + cond2 + " " + cond1 + "'s";
		Enemy e = playerMove.enemy;
		if (e != null)
		{
			playerMove.lockMovement(true);
			messageToSay = e.message;
		}
		

		if (Input.GetKeyDown("space")){
			if(Vector3.Distance(playerMove.transform.position, playerMove.currentChunk.transform.GetComponentInChildren(typeof(empty)).transform.position) < playerMove.completedDistance/200f){
				UnFreezePlayer();
				ClearMessage();
				battle.startBattle(playerMove.enemy.gameObject);
				playerMove.enemy = null;
				mainText.text = null;
				
			}
		}
		}
	}

	public void ClearMessage() {
		messageToSay = null;
	}
	public void ToggleControls() {
		playerMove.enabled = !playerMove.enabled;
	}
	public void FreezePlayer() {
		playerMove.lockMovement(true);
		playerMove.gate = false;
		//playerMove.enabled = false;
	}
	public void UnFreezePlayer()
	{
		playerMove.lockMovement(false);
		playerMove.gate = true;
		//playerMove.enabled = true;
	}
	void ReadCheckPointValues(diceCheckpoint c, out string a, out string b) {
		a =b = null;
		if (c.evenCheck) 
		{
			a = "evens";
			b = "" + c.evenCount;
		}
		if (c.oddCheck)
		{
			a = "odds";
			b = "" + c.oddCount;
		}
		if (c.oneCheck) 
		{
			a = "ones";
			b = ""+ c.oneCount;
		}
		if (c.twoCheck)
		{
			a = "twos";
			b = "" + c.twoCount;
		}
		if (c.threeCheck)
		{
			a = "threes";
			b = "" + c.threeCount;
		}
		if (c.fourCheck)
		{
			a = "fours";
			b = "" + c.fourCount;
		}
		if (c.fiveCheck)
		{
			a = "fives";
			b = "" + c.fiveCount;
		}
		if (c.sixCheck)
		{
			a = "sixes";
			b = "" + c.sixCount;
		}
		
	}

}
