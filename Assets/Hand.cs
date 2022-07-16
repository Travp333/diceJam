using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[SerializeField]
	public int damage = 15;
	[SerializeField]
	diceCheckpoint check;
	[SerializeField]
	fellaAnimController anim;
	[SerializeField]
	public bool isPlayer;
	[SerializeField]
	float tick = 1f;
	[SerializeField]
	GameObject prefabDice;
	[SerializeField]
	int diceAmount = 10;
	List<diceRoll> diceList;
	public int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount, totalCount;
	float tickCounter=0f;
	[SerializeField]
	float throwIntensity = 5f;
	[SerializeField]
	float spinIntensity = 5f;
	bool enemySleepDice = false;
	bool blocker;

	//called when you match the opponents request, resets hand and dishes out rewards, ends battle if necessary
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
		diceList = new List<diceRoll>();
		ClearCounts();
		//SpawnDice(diceAmount);

	}
	void Tick()
	{
		UpdateTotals();

	}
	private void UpdateTotals() {
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

	public void enemyReRoll(){
		if(!blocker){
			enemySleepDice = true;
			foreach(diceRoll d in diceList){
				if(!d.gameObject.GetComponent<Rigidbody>().IsSleeping()){
					enemySleepDice = false;
				}
			}
			if(enemySleepDice){
				enemySleepDice = false;
				Debug.Log("ENEMYREROLL");
				anim.setthrowinDie();
				blocker = true;
			}
		}
	}

	public void reRoll(){
		check.openGate();
		blocker = false;
		ClearCounts();
		foreach (diceRoll d in diceList){
			Destroy(d.gameObject);
		}
		diceList.Clear();
		SpawnDice(diceAmount);
	}


	private void Update()
	{
		tickCounter += Time.deltaTime;
		if (tickCounter > tick) {
			tickCounter -= tick;
			Tick();
			if(!isPlayer){
				enemyReRoll();
			}
		}
		if(Input.GetKeyDown("r")){
			if(isPlayer){
				reRoll();
			}
		}

	}
	public void SpawnDice(int quantity) {
		if(!isPlayer){
			enemySleepDice = true;
		}
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
