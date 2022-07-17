using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool spoke;
	public bool isDoor;
	public int doorCost;
	public bool chill;
	[SerializeField]
	public bool specialEnemy;
	public bool block;
	public string message = " bleh! ";
	public enum enemyType {boar, turtle};
	[SerializeField]
	public enemyType type = default;
	public void openDoors(){
		this.transform.parent.gameObject.GetComponent<dungeonPiece>().isLocked = false;
		Destroy(this.transform.parent.GetChild(18).gameObject);
	}

}
