using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public string message = " bleh! ";
	public enum enemyType {boar, turtle};
	[SerializeField]
	public enemyType type = default;

}
