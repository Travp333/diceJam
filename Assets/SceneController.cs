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

		Enemy e = playerMove.enemy;
		if (e != null)
		{
			playerMove.lockMovement(true);
			messageToSay = e.message;
		}
		else 
		{
			ClearMessage();
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
}
