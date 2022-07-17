using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//dungeon overworld script controls the character and the camera
public class movement : MonoBehaviour
{   manAnimController man;
    [SerializeField]
    Camera battleCam;
    [SerializeField]
    GameObject start;
    SceneController scene;
    
    [SerializeField]
    float lerpTime;
    float countdown;
    [SerializeField]
    public GameObject currentChunk;
    public bool gate = true;
    [SerializeField]
    public float completedDistance;
    //camera stuff
    [SerializeField]
    Camera mainCam = default;
    [SerializeField]
    Camera baseCam = default;
    public Enemy enemy = null;
    public Camera specialCam = null;
    public float camLerpTime;
    float camCountdown;
    bool dialogueBlock;
    bool lerpBlock = false;
    void Start()
    {
        man = this.gameObject.transform.GetChild(3).GetComponent<manAnimController>();
        this.transform.position = currentChunk.transform.GetComponentInChildren(typeof(empty)).transform.position;
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
            if(g.GetComponent<SceneController>() != null){
                scene = g.GetComponent<SceneController>();
            }
        }
    }

    public void backToStart(){
        transform.position = start.transform.GetComponentInChildren(typeof(empty)).transform.position;
        currentChunk = start;
        specialCam = null;
        //mainCam.transform.position = baseCam.transform.position;
        //LerpTransform(mainCam.transform, baseCam.transform, 100);
    }
    public void startBattle(){
        lerpBlock = true;
        LerpTransform(mainCam.transform, battleCam.transform, 1);
        LerpFOV(mainCam, battleCam, 1);
        //mainCam.transform.position = battleCam.transform.position;
    }
    public void endBattle(){

        lerpBlock = false;
        LerpTransform(mainCam.transform, baseCam.transform, 1);
        LerpFOV(mainCam, baseCam, 1);
        mainCam.transform.position = baseCam.transform.position;
    }

    public void lockMovement(bool plug){
        man.setMoving(false);
        dialogueBlock = plug;
    }


    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(this.transform.position, currentChunk.transform.GetComponentInChildren(typeof(empty)).transform.position) < completedDistance && !dialogueBlock){
            gate = true;
            man.setMoving(false);
        }
        else{
            gate = false;
        }
        if(countdown < lerpTime){
            countdown += Time.deltaTime;
        }
        if(!lerpBlock){
            transform.position = Vector3.Lerp(transform.position, currentChunk.transform.GetComponentInChildren(typeof(empty)).transform.position, countdown);
        }
        if(gate){
            if(Input.GetKeyDown("w")){
                if(currentChunk.GetComponent<dungeonPiece>().northPiece != null){
                    if(!currentChunk.GetComponent<dungeonPiece>().northPiece.GetComponent<dungeonPiece>().southDoor.activeSelf && !currentChunk.GetComponent<dungeonPiece>().northDoor.activeSelf){
                        man.setMoving(true);
                        currentChunk = currentChunk.GetComponent<dungeonPiece>().northPiece; 
                        countdown = 0;
                        camCountdown = 0;
                        CheckforSpecialCamera();
                        CheckforEnemy();
                        if(enemy != null){
                            enemy.spoke = false;
                        }
                        
                    }

                }
            }
            if(Input.GetKeyDown("a")){
                if(currentChunk.GetComponent<dungeonPiece>().westPiece != null){
                    if(!currentChunk.GetComponent<dungeonPiece>().westPiece.GetComponent<dungeonPiece>().eastDoor.gameObject.activeSelf && !currentChunk.GetComponent<dungeonPiece>().westDoor.activeSelf){
                        man.setMoving(true);
                        currentChunk = currentChunk.GetComponent<dungeonPiece>().westPiece;   
                        countdown = 0;
                        camCountdown = 0;
                        CheckforSpecialCamera();
                        CheckforEnemy();
                        if(enemy != null){
                            enemy.spoke = false;
                        }
                    }

                }
            }
            if(Input.GetKeyDown("s")){
                if(currentChunk.GetComponent<dungeonPiece>().southPiece != null){
                    if(!currentChunk.GetComponent<dungeonPiece>().southPiece.GetComponent<dungeonPiece>().northDoor.gameObject.activeSelf && !currentChunk.GetComponent<dungeonPiece>().southDoor.activeSelf){
                        man.setMoving(true);
                        currentChunk = currentChunk.GetComponent<dungeonPiece>().southPiece;  
                        countdown = 0;
                        camCountdown = 0;
                        CheckforSpecialCamera();
                        CheckforEnemy();
                        if(enemy != null){
                            enemy.spoke = false;
                        }
                    }

                }
            }
            if(Input.GetKeyDown("d")){
                if(currentChunk.GetComponent<dungeonPiece>().eastPiece != null){
                    if(!currentChunk.GetComponent<dungeonPiece>().eastPiece.GetComponent<dungeonPiece>().westDoor.gameObject.activeSelf && !currentChunk.GetComponent<dungeonPiece>().eastDoor.activeSelf){
                        man.setMoving(true);
                        currentChunk = currentChunk.GetComponent<dungeonPiece>().eastPiece;  
                        countdown = 0;
                        camCountdown = 0;
                        CheckforSpecialCamera();
                        CheckforEnemy();
                        if(enemy != null){
                            enemy.spoke = false;
                        }
                    }

                }
            }
        }
        
        if (specialCam != null && !lerpBlock)
        {
            if (camCountdown < camLerpTime)
            {
                camCountdown += Time.deltaTime;
            }
            LerpTransform(mainCam.transform, specialCam.transform, camCountdown);
            LerpFOV(mainCam, specialCam, countdown);
        }
        if(specialCam == null && !lerpBlock)
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
    void CheckforEnemy() {
       enemy =  currentChunk.GetComponentInChildren(typeof(Enemy)) as Enemy;
        if (enemy != null) {
            Debug.Log("Enemy Found");
        }
    }
    
}
