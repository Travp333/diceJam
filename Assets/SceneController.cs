using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI mainText = default;
	public float tickLength = .5f;
	float tickCount = 0;
    public string messageToSay;
	[SerializeField]
	movement playerMove = default;

	
	
	private void Update()
	{
		
			if (messageToSay != null)
			{
				mainText.text = messageToSay;
			}
			Enemy e = playerMove.enemy;
		if (e != null)
		{

			messageToSay = e.message;
		}
		else 
		{
			ClearMessage();
		}
			
		
		

		if (Input.GetKeyDown("space")){
			UnFreezePlayer();
			ClearMessage();
		}
	}

	public void ClearMessage() {
		messageToSay = "";
	}
	public void ToggleControls() {
		playerMove.enabled = !playerMove.enabled;
	}
	public void FreezePlayer() {
		playerMove.enabled = false;
	}
	public void UnFreezePlayer()
	{
		playerMove.enabled = true;
	}
}
