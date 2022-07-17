using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
	[SerializeField]
	GameObject button;
	[SerializeField]
	playerStats stats;
	battleController battle;
	[SerializeField]
    TextMeshProUGUI dieCount = default;
    [SerializeField]
    TextMeshProUGUI mainText = default;
	[SerializeField]
	TextMeshProUGUI subTitle = default;
	public float tickLength = .5f;
	float tickCount = 0;
    public string messageToSay;
	public string messageToSay2;
	
	[SerializeField]
	movement playerMove = default;
	diceCheckpoint ch = default;
	public bool battleScene = false;
	bool hasSpoke;
	Enemy e;
	[SerializeField]
	public void reset(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public AudioSource audioSource = default;
	

	public void updateHasSpoke(){
		if(e != null){
			hasSpoke = e.spoke;
		}
		else{
			hasSpoke = false;
		}
	}
	public void setHasSpoke(bool plug){
		hasSpoke = plug;
	}

	void Start()
	{
		if(e != null){
			hasSpoke = e.spoke;
		}
		else{
			hasSpoke = false;
		}
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
            if(g.GetComponent<battleController>() != null){
                battle = g.GetComponent<battleController>();
            }
        }
	}
	public void setCheckPoint(GameObject g){
		ch = battle.whichMonsterBattling(g).GetComponent<diceCheckpoint>();
	}
	private void LateUpdate()
	{
		
	}

	private void Update()
	{
		dieCount.text = stats.diceAmount.ToString();

		if (messageToSay != null)
		{
			mainText.text = messageToSay;
			subTitle.text = messageToSay2;
		}
		if (battleScene) {
			button.SetActive(true);
			string cond1 = null;
			string cond2;
			ReadCheckPointValues(ch, out cond1, out cond2);
			if(ch.hp > 0){
				messageToSay = "Roll " + cond2 + " " + cond1 + "'s";
			}
			else{
				messageToSay = "Won the Fight!";
			}

			transform.GetChild(3).gameObject.SetActive(true);
			
		}
		else{
			transform.GetChild(3).gameObject.SetActive(false);
		}
		e = playerMove.enemy;
		
		if (e != null)
		{
			playerMove.lockMovement(true);
			messageToSay2 = e.message;
		}
		
		updateHasSpoke();
		if (Input.GetKeyDown("space") && !battleScene && !hasSpoke){
			if(Vector3.Distance(playerMove.transform.position, playerMove.currentChunk.transform.GetComponentInChildren(typeof(empty)).transform.position) < playerMove.completedDistance/200f && !e.chill && !e.isDoor){
				setHasSpoke(true);
				ClearMessage();
				battle.startBattle(playerMove.enemy.gameObject);
				playerMove.enemy = null;
				mainText.text = null;
				button.SetActive(false);
				
			}
			else if (e.chill && !e.isDoor){
				setHasSpoke(true);
				ClearMessage();
				playerMove.enemy = null;
				mainText.text = null;
				UnFreezePlayer();
				button.SetActive(false);
			}
			else if (e.isDoor){
				setHasSpoke(true);
				if(e.doorCost > playerMove.gameObject.GetComponent<playerStats>().diceAmount){
					//Debug.Log("Not enough Cash!");
					ClearMessage();
					playerMove.enemy = null;
					mainText.text = null;
					UnFreezePlayer();
					button.SetActive(false);
				}
				else{
					//Debug.Log("Door opened!");
					// in case we want to make them pay dice to continue
					//playerMove.gameObject.GetComponent<playerStats>().diceAmount -= e.doorCost;
					e.openDoors();
					ClearMessage();
					playerMove.enemy = null;
					mainText.text = null;
					UnFreezePlayer();
					button.SetActive(false);
				}
			}
		}
	}

	public void ClearMessage() {
		//Debug.Log("Clearing text...");
		messageToSay = "";
		messageToSay2 = "";
		mainText.text = "";
		subTitle.text = "";
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
		if(battleScene){
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

}
