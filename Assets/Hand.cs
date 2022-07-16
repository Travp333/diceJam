using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[SerializeField]
	GameObject prefabDice;
	[SerializeField]
	int diceAmount = 10;
	List<diceRoll> diceList;
	int evenCount, oddCount, oneCount, twoCount, threeCount, fourCount, fiveCount, sixCount, totalCount;
	float tickCounter=0f;
	[SerializeField]
	float throwIntensity = 5f;
	[SerializeField]
	float spinIntensity = 5f;
	
	private void Start()
	{
		diceList = new List<diceRoll>();
		ClearCounts();
		SpawnDice(diceAmount);

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
						Debug.Log("dice read failed");
						break;
			}
		}
		Debug.Log("EvenCount =" +  evenCount + "  OddCount =" + oddCount);
		Debug.Log(oneCount + " 1's, " + twoCount + " 2's, " + threeCount + " 3's, " + fourCount + " 4's, " + fiveCount + " 5's, " + sixCount + " 6's, ");
	}
	private void ClearCounts() {
		evenCount = oddCount = oneCount = twoCount = threeCount = fourCount = fiveCount = sixCount = totalCount = 0;
	}
	private void Update()
	{
		tickCounter += Time.deltaTime;
		if (tickCounter > 2f) {
			tickCounter -= 2f;
			Tick();
		}
	}

	void SpawnDice(int quantity) {
		for (int i = 0; i < quantity; i++)
		{

			GameObject dice = Instantiate(prefabDice, this.transform.position + (Vector3.right * i) + Vector3.right, Quaternion.identity);

			Rigidbody r = dice.GetComponent<Rigidbody>();
			r.AddForce(this.transform.up * throwIntensity*100);
			r.AddTorque( Random.onUnitSphere * 1000 * spinIntensity);

			diceList.Add(dice.GetComponent<diceRoll>());
		}
	}
}
