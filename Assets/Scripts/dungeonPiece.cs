using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonPiece : MonoBehaviour
{
    public bool isLocked;
    [SerializeField]
    public GameObject southPiece, northPiece, eastPiece, westPiece;
    [SerializeField]
    public GameObject southDoor, northDoor, eastDoor, westDoor;

    void FixedUpdate()
    {
        if(!isLocked){
            if(southDoor!=null){
                southDoor.SetActive(false);
            }
            if(northDoor!=null){
                northDoor.SetActive(false);
            }
            if(eastDoor!=null){
                eastDoor.SetActive(false);
            }
            if(westDoor!=null){
                westDoor.SetActive(false);
            }



        }
    }
}
