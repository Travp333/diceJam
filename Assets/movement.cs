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


    //camera stuff
    [SerializeField]
    Camera mainCam = default;
    [SerializeField]
    Camera baseCam = default;
  

    

    public Camera specialCam = null;
    public float camLerpTime;
    float camCountdown;
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
                    camCountdown = 0;
                    CheckforSpecialCamera();
                }
            }
            if(Input.GetKeyDown("a")){
                if(currentChunk.GetComponent<dungeonPiece>().westPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().westPiece;   
                    countdown = 0;
                    camCountdown = 0;
                    CheckforSpecialCamera();
                }
            }
            if(Input.GetKeyDown("s")){
                if(currentChunk.GetComponent<dungeonPiece>().southPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().southPiece;  
                    countdown = 0;
                    camCountdown = 0;
                    CheckforSpecialCamera(); 
                }
            }
            if(Input.GetKeyDown("d")){
                if(currentChunk.GetComponent<dungeonPiece>().eastPiece != null){
                    currentChunk = currentChunk.GetComponent<dungeonPiece>().eastPiece;  
                    countdown = 0;
                    camCountdown = 0;
                    CheckforSpecialCamera();
                }
            }
        }
        
        if (specialCam != null)
        {
            if (camCountdown < camLerpTime)
            {
                camCountdown += Time.deltaTime;
            }
            LerpTransform(mainCam.transform, specialCam.transform, camCountdown);
            LerpFOV(mainCam, specialCam, countdown);
        }
        if(specialCam == null)
        {
            if (camCountdown < camLerpTime)
            {
                camCountdown += Time.deltaTime;
            }
            LerpTransform(mainCam.transform, baseCam.transform, camCountdown);
            
            LerpFOV(mainCam, baseCam, camCountdown);
        }
    }
    void LerpTransform(Transform a, Transform b, float countdown) {
        a.position = Vector3.Lerp(a.position, b.position, countdown);
        a.rotation = Quaternion.Euler(Vector3.Lerp(a.rotation.eulerAngles, b.rotation.eulerAngles, countdown));
        a.localScale = Vector3.Lerp(a.localScale, b.localScale, countdown); 
        
    }
    void LerpFOV(Camera a, Camera b, float countdown) {
       a.fieldOfView = Mathf.Lerp(a.fieldOfView, b.fieldOfView, countdown);

    }
    void LerpFOV(Camera a, float b, float countdown)
    {
        a.fieldOfView = Mathf.Lerp(a.fieldOfView, b, countdown);

    }
    void CheckforSpecialCamera() {
        specialCam = currentChunk.GetComponentInChildren(typeof(Camera), true) as Camera;
        if(specialCam == null)
        Debug.Log("No Special Cam");
    }
}
