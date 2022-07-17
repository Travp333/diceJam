using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// spawns die and keeps track of what face they land on
public class Hand : MonoBehaviour
{
	SceneController scene;
	movement move;
	battleController battle;
	//reference to the player stats script
	playerStats stats;
	// reference to the checkpoint script
	diceCheckpoint check;
	// reference to the anim script
	public fellaAnimController anim;
	// bool that tracks whether this hand is placed on the player or not, manually set
	[SerializeField]
	public bool isPlayer;
	//how often the script updates the count of die and their faces
	[SerializeField]
	float tick = .1f;
	// prefab spawned when die is thrown
	[SerializeField]
	GameObject prefabDice;
	// list of all dice thrown by this hand
	List<diceRoll> diceList;
	//variables tracking amounts of each type of die
	public int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount, totalCount;
	//idk???
	float tickCounter=0f;
	[SerializeField]
	// how hard the die are thrown
	float throwIntensity = 5f;
	[SerializeField]
	// how much they spin
	float spinIntensity = 5f;
	// checks if the enemies die are all asleep
	bool enemySleepDice = false;
	// used to help track sleeping die
	bool blocker;
	public bool blocker2;

	//called when you match the given request
	public void Success(){
		Debug.Log(this.transform.parent.gameObject.name + " Won!");
		ClearCounts();
		foreach (diceRoll d in diceList){
			Destroy(d.gameObject);
		}
		diceList.Clear();
	}

	

	
	private void Start()
	{
		// only need a reference to the anim controller if its the enemy, otherwise it can be null
		if(!isPlayer && transform.parent.GetComponent<fellaAnimController>() != null){
			anim = transform.parent.GetComponent<fellaAnimController>();
		}
		//plugs in all necessary references
		foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
			if(g.GetComponent<playerStats>()!= null){
				stats = g.GetComponent<playerStats>();
			}
			if(g.GetComponent<battleController>()!= null){
				battle = g.GetComponent<battleController>();
			}
			if(g.GetComponent<movement>()!= null){
				move = g.GetComponent<movement>();
			}
			if(g.GetComponent<SceneController>()!= null){
				scene = g.GetComponent<SceneController>();
			}
		}
		if(!isPlayer){
			check = transform.parent.GetComponent<diceCheckpoint>();
		}
		diceList = new List<diceRoll>();
		ClearCounts();
		//SpawnDice(diceAmount);

	}

	public void updateCheckReference(diceCheckpoint ch){
		check = ch;
	}
	void Tick()
	{
		AddDiceToCount();

	}
	private void AddDiceToCount() {
		foreach (diceRoll d in diceList) {
			int f = d.currentFace;
			if(!d.wasCounted)
			switch (f) {

				case 1:
						oneCount++;
						oddCount++;
						d.wasCounted = true;
						break;
				case 2:
						twoCount++;
						evenCount++;
						d.wasCounted = true;
						break;
				case 3: 
						threeCount++;
						oddCount++;
						d.wasCounted = true;
						break;
				case 4: 
						fourCount++;
						evenCount++;
						d.wasCounted = true;
						break;
				case 5: 
						fiveCount++;
						oddCount++;
						d.wasCounted = true;
						break;
				case 6: 
						sixCount++;
						evenCount++;
						d.wasCounted = true;
						break;
				default :
						d.wasCounted = false;
						//Debug.Log("dice read failed");
						break;
			}
		}
		//Debug.Log("EvenCount =" +  evenCount + "  OddCount =" + oddCount);
		//Debug.Log(oneCount + " 1's, " + twoCount + " 2's, " + threeCount + " 3's, " + fourCount + " 4's, " + fiveCount + " 5's, " + sixCount + " 6's, ");
	}
	private void ClearCounts() {
		evenCount = oddCount = oneCount = twoCount = threeCount = fourCount = fiveCount = sixCount = totalCount = 0;

	}
	// checks if enemy die are all asleep. if so, it plays the animation that then spawns new die by calling reRoll
	public void enemyReRoll(){
		if(scene.battleScene){
			if(!blocker && !check.stunned && !anim.getHappy()){
				enemySleepDice = true;
				foreach(diceRoll d in diceList){
					if(!d.gameObject.GetComponent<Rigidbody>().IsSleeping()){
						enemySleepDice = false;
					}
				}
				if(enemySleepDice){
					enemySleepDice = false;
					//Debug.Log("ENEMYREROLL");
					Invoke("setthrowinDie", .1f);
					//anim.setthrowinDie();
					blocker = true;
				}
			}
		}
	}
	void setthrowinDie(){
		anim.setthrowinDie();
	}
	//clears all dice
	public void clearDie(){
		foreach (diceRoll d in diceList){
			Destroy(d.gameObject);
		}
		diceList.Clear();
	}
	// deletes old die, updates the list, then spawns new ones by calling spawnDice
	public void reRoll(){
		if(scene.battleScene){
			if(isPlayer && !stats.stunned){
				check.openGate();
			}
			else if(!isPlayer && !check.stunned && !anim.getHappy()){
				check.openGate2();
			}
			blocker = false;
			ClearCounts();
			//only run for player so that this can be ran on animation for enemy
			if(isPlayer){
				foreach (diceRoll d in diceList){
					Destroy(d.gameObject);
				}
				diceList.Clear();
				
			}
			if(isPlayer&& !stats.stunned){
				SpawnDice(stats.diceAmount);
			}
			else if (!isPlayer && !check.stunned && !anim.getHappy()){
				SpawnDice(check.diceCount);
			}
		}
	}

	public void setBlocker(){
		blocker2 = true;
	}
	public void resetBlocker(){
		blocker2 = false;
	}


	private void Update()
	{
		//counter that checks die status every set amount of time
		tickCounter += Time.deltaTime;
		if (tickCounter > tick) {
			tickCounter -= tick;
			Tick();
			if(scene.battleScene){
				if(!isPlayer && !check.stunned && !anim.getHappy()){
					enemyReRoll();
				}
			}
		}
		// re-rolls your current hand if you are able to roll
		if(!blocker2){
			if(Input.GetKeyDown("r")){
				if(isPlayer && !stats.stunned){
					reRoll();
				}
			}
		}


	}
	//creates new die prefabs and updates the list i had to split it up to be different between player and enemy rolls to allow stun to work properly, may not totally be necessary though
	public void SpawnDice(int quantity) {
		if(!isPlayer){
			if(!check.stunned && !anim.getHappy()){
				enemySleepDice = true;
				for (int i = 0; i < quantity; i++)
				{
					GameObject dice = Instantiate(prefabDice, this.transform.position + (Vector3.up * (i * 1.5f)) - Vector3.up + (Vector3.right*Random.Range(-3f, 3f)),  Random.rotation);//Quaternion.identity);
					Rigidbody r = dice.GetComponent<Rigidbody>();
					r.AddForce(this.transform.up * throwIntensity*100);
					r.AddTorque( Random.onUnitSphere * 1000 * spinIntensity);
					diceList.Add(dice.GetComponent<diceRoll>());
					
				}
			}
		}
		else if (isPlayer && !stats.stunned){
				for (int i = 0; i < quantity; i++)
				{
					GameObject dice = Instantiate(prefabDice, this.transform.position + (Vector3.up * (i * 1.5f)) - Vector3.up + (Vector3.right*Random.Range(-3f, 3f)),  Random.rotation);//Quaternion.identity);
					Rigidbody r = dice.GetComponent<Rigidbody>();
					r.AddForce(this.transform.up * throwIntensity*100);
					r.AddTorque( Random.onUnitSphere * 1000 * spinIntensity);
					diceList.Add(dice.GetComponent<diceRoll>());
				//Debug.Log(quantity + " spawned");
			}
		}
	}
}
