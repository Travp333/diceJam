using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    float lerpTime;
    float countdown;
    [SerializeField]
    GameObject currentChunk;
    bool gate = true;
    [SerializeField]
    float completedDistance;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = currentChunk.transform.GetChild(13).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, currentChunk.transform.GetChild(13).transform.position) < completedDistance){
            gate = true;
        }
        else{
            gate = false;
        }
        if(countdown < lerpTime){
            countdown += Time.deltaTime;
        }
        transform.position = Vector3.Lerp(transform.position, currentChunk.transform.GetChild(13).transform.position, countdown);
        if(gate){
            if(Input.GetKeyDown("w")){
                if(currentChunk.GetComponent<dungeonPiece>().northPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().northPiece; 
                    countdown = 0;  
                }
            }
            if(Input.GetKeyDown("a")){
                if(currentChunk.GetComponent<dungeonPiece>().westPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().westPiece;   
                    countdown = 0; 
                }
            }
            if(Input.GetKeyDown("s")){
                if(currentChunk.GetComponent<dungeonPiece>().southPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().southPiece;  
                    countdown = 0;  
                }
            }
            if(Input.GetKeyDown("d")){
                if(currentChunk.GetComponent<dungeonPiece>().eastPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().eastPiece;  
                    countdown = 0;  
                }
            }
        }
    }
}
