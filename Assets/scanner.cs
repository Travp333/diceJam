using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    [SerializeField]
    bool isNorth, isEast, isWest, isSouth;
    [SerializeField]
    bool hit = false;

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "dungeon" && other.gameObject != this.transform.root.gameObject){
            Debug.Log("TEST");
            hit = true;
            if(isNorth){
                this.transform.root.gameObject.GetComponent<DungeonGenerator>().blockNorth(other.transform.root.gameObject);
                DestroySelf();
            }
            else if(isEast){
                this.transform.root.gameObject.GetComponent<DungeonGenerator>().blockEast(other.transform.root.gameObject);
                DestroySelf();
            }
            else if(isSouth){
                this.transform.root.gameObject.GetComponent<DungeonGenerator>().blockSouth(other.transform.root.gameObject);
                DestroySelf();
            }
            else if(isWest){
                this.transform.root.gameObject.GetComponent<DungeonGenerator>().blockWest(other.transform.root.gameObject);
                DestroySelf();
            }
            
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "dungeon"){
    //        hit = false;
    //    }
    //}
    //void FixedUpdate()
    //{
    //    isEmpty = true;
   // }
   // void OnTriggerStay(Collider other)
   // {
    //    isEmpty = false;
   // }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    //void Start()
    //{
    //    Invoke("DestroySelf", 1f);
    //}
    
    public void DestroySelf(){
        this.gameObject.SetActive(false);
    }


}
