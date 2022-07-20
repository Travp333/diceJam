using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    List<scanner> scanners;
    dungeonSizeController size;
    [SerializeField]
    GameObject root;
    [SerializeField]
    GameObject newPiece;
    [SerializeField]
    List<GameObject> northPieces;
    [SerializeField]
    List<GameObject> southPieces;
    [SerializeField]
    List<GameObject> westPieces;
    [SerializeField]
    List<GameObject> eastPieces;
    public List<List<GameObject>> listlist = new List<List<GameObject>>();
    public GameObject touchingSouth, touchingNorth, touchingEast, touchingWest;
    [SerializeField]
    bool northBlocked = false, southBlocked= false, westBlocked= false, eastBlocked= false;
    int random;

    void StartGeneration()
    {
        expand();
    }
    public void blockEast(GameObject g){
        eastBlocked = true;
        touchingEast = g;
    }
    public void blockNorth(GameObject g){
        northBlocked = true;
        touchingNorth = g;
    }
    public void blockWest(GameObject g){
        westBlocked = true;
        touchingWest = g;
    }
    public void blockSouth(GameObject g){
        eastBlocked = true;
        touchingSouth = g;
    }
    //void scanTouching(){
    //    probe = InstantiateToTheEast(scanner).GetComponent<scanner>();
    //    probe.transform.Rotate(-90, 0, 0);
    //    if(!probe.isEmpty){
   //         Debug.Log("Blocked to the East!");
    //        eastBlocked = true;
            //probe.GetComponent<scanner>().touchingPiece.GetComponent<DungeonGenerator>()
    //    }
    //    Destroy(probe.gameObject);
    //    probe = InstantiateToTheWest(scanner).GetComponent<scanner>();
    //    probe.transform.Rotate(-90, 0, 0);
    //    if(!probe.isEmpty){
    //        Debug.Log("Blocked to the West!");
    //        westBlocked = true;
            //probe.GetComponent<scanner>().touchingPiece.GetComponent<DungeonGenerator>()
    //    }
    //    Destroy(probe.gameObject);
    //    probe = InstantiateToTheNorth(scanner).GetComponent<scanner>();
     //   probe.transform.Rotate(-90, 0, 0);
    //    if(!probe.isEmpty){
    //        Debug.Log("Blocked to the North!");
    //        northBlocked = true;
            //probe.GetComponent<scanner>().touchingPiece.GetComponent<DungeonGenerator>()
    //    }
    //    Destroy(probe.gameObject);
    //    probe = InstantiateToTheSouth(scanner).GetComponent<scanner>();
    //    probe.transform.Rotate(-90, 0, 0);
    //    if(!probe.isEmpty){
    //        Debug.Log("Blocked to the South!");
    //        southBlocked = true;
            //probe.GetComponent<scanner>().touchingPiece.GetComponent<DungeonGenerator>()
    //    }
    //    Destroy(probe.gameObject);
   // }

   /// <summary>
   /// Start is called on the frame when a script is enabled just before
   /// any of the Update methods is called the first time.
   /// </summary>
   void Start()
   {
        size = GameObject.FindObjectsOfType<dungeonSizeController>()[0];
        //listlist.Add(northPieces);
        //listlist.Add(southPieces);
        //listlist.Add(westPieces);
        //listlist.Add(eastPieces);
   }

    void clearWalls(){
        //Debug.Log("Clearing Old Walls");
        foreach(List<GameObject> l in listlist){
            foreach (GameObject m in l ){
                m.SetActive(false);
            }
        }
        if(touchingEast == null){
            eastBlocked = false;
        }
        if(touchingNorth == null){
            northBlocked = false;
        }
        if(touchingWest == null){
            westBlocked = false;
        }
        if(touchingSouth == null){
            southBlocked = false;
        }


        
    }
    void buildWalls(){

        clearWalls();

        listlist.Clear();
        listlist.Add(northPieces);
        listlist.Add(southPieces);
        listlist.Add(westPieces);
        listlist.Add(eastPieces);

        
        foreach(List<GameObject> l in listlist){
            random = Random.Range(0, 4);
            if(random == 0){
                //wall
                l[0].SetActive(true);
                if(l[0].name == "NorthWall"){
                    northBlocked = true;
                }
                else if(l[0].name == "SouthWall"){
                    southBlocked = true;
                }
                else if(l[0].name == "EastWall"){
                    eastBlocked = true;
                }
                else if(l[0].name == "WestWall"){
                    westBlocked = true;
                }
            }
            else if(random == 1){
                //doorway, closed
                l[1].SetActive(true);
                l[2].SetActive(true);

                if(l[2].name == "NorthWallDoor"){
                    northBlocked = true;
                }
                else if(l[2].name == "SouthWallDoor"){
                    southBlocked = true;
                }
                else if(l[2].name == "EastWallDoor"){
                    eastBlocked = true;
                }
                else if(l[2].name == "WestWallDoor"){
                    westBlocked = true;
                }
            }
            else if(random == 2){
                //doorway, open
                l[1].SetActive(true);
            }
            else if(random == 3){
                //open w/ plug
                l[3].SetActive(true);
            }
        }
    }

    void expandNorth(){
        if(!northBlocked){
            DungeonGenerator gn;
            gn = InstantiateToTheNorth(newPiece).GetComponent<DungeonGenerator>();

            gn.clearTouching();

            gn.buildWalls();
            gn.southBlocked = true;
        
            northBlocked = true;
            touchingNorth = gn.gameObject;
            gn.touchingSouth = this.gameObject;
            scanners[2].DestroySelf();
            gn.scanners[1].DestroySelf();

            size.incrementChunkCount();
        }
    }
    void expandSouth(){
        if(!southBlocked){
            DungeonGenerator gs;
            gs = InstantiateToTheSouth(newPiece).GetComponent<DungeonGenerator>();

            gs.clearTouching();

            gs.buildWalls();
            gs.northBlocked = true;

            
            southBlocked = true;
            touchingSouth = gs.gameObject;
            gs.touchingNorth = this.gameObject;
            scanners[1].DestroySelf();
            gs.scanners[2].DestroySelf();

            size.incrementChunkCount();
        }
    }
    void expandWest(){
        if(!westBlocked){
            DungeonGenerator gw;
            gw = InstantiateToTheWest(newPiece).GetComponent<DungeonGenerator>();

            gw.clearTouching();

            gw.buildWalls();
            gw.eastBlocked = true;

            
            westBlocked = true;
            touchingWest = gw.gameObject;
            gw.touchingEast = this.gameObject;
            scanners[0].DestroySelf();
            gw.scanners[3].DestroySelf();

            size.incrementChunkCount();
        }
    }

    void expandEast(){
        if(!eastBlocked){
            DungeonGenerator ge;
            ge = InstantiateToTheEast(newPiece).GetComponent<DungeonGenerator>();

            ge.clearTouching();

            ge.buildWalls();
            ge.westBlocked = true;


            eastBlocked = true;
            touchingEast = ge.gameObject;
            ge.touchingWest = this.gameObject;
            scanners[3].DestroySelf();
            ge.scanners[0].DestroySelf();

            size.incrementChunkCount();
        }
    }

    void clearTouching(){
        touchingEast = null;
        touchingNorth = null;
        touchingSouth = null;
        touchingEast = null;
    }
    void expand(){
        if(size.chunkCount < size.maxChunks){

            //this is so that it doesnt bias towards one direction
            random = Random.Range(0, 4);
            if(random == 0){
                expandNorth();
                expandSouth();
                expandWest();
                expandEast();
            }
            else if(random == 1){
                expandSouth();
                expandNorth();
                expandWest();
                expandEast();
            }
            else if(random == 2){
                expandEast();
                expandSouth();
                expandWest();
                expandNorth();
            }
            else if(random == 3){
                expandWest();
                expandSouth();
                expandEast();
                expandNorth();
            }
        }
    }



    //goes west
     GameObject InstantiateToTheWest(GameObject prefab) {
      Transform t = this.transform;
      Mesh m = root.GetComponent<MeshFilter>().mesh;
      // spawn prefab object next to original (in the x direction)
      float x = m.bounds.size.x * t.localScale.x;
      return Instantiate(prefab, t.position + new Vector3(x,0,0), Quaternion.identity);
      //touchingWest.GetComponent<DungeonGenerator>().eastBlocked = true;
    }
     GameObject InstantiateToTheNorth(GameObject prefab) {
      Transform t = this.transform;
      Mesh m = root.GetComponent<MeshFilter>().mesh;
      // spawn prefab object next to original (in the x direction)
      float x = m.bounds.size.x * t.localScale.x;
      return Instantiate(prefab, t.position - new Vector3(0,0,x), Quaternion.identity);
      //touchingNorth.GetComponent<DungeonGenerator>().southBlocked = true;
    }
     GameObject InstantiateToTheSouth(GameObject prefab) {
      Transform t = this.transform;
      Mesh m = root.GetComponent<MeshFilter>().mesh;
      // spawn prefab object next to original (in the x direction)
      float x = m.bounds.size.x * t.localScale.x;
      return Instantiate(prefab, t.position + new Vector3(0,0,x), Quaternion.identity);
      //touchingSouth.GetComponent<DungeonGenerator>().northBlocked = true;
    }
     GameObject InstantiateToTheEast(GameObject prefab) {
      Transform t = this.transform;
      Mesh m = root.GetComponent<MeshFilter>().mesh;
      // spawn prefab object next to original (in the x direction)
      float x = m.bounds.size.x * t.localScale.x;
      return Instantiate(prefab, t.position - new Vector3(x,0,0), Quaternion.identity);
      //touchingEast.GetComponent<DungeonGenerator>().westBlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if(probe != null){
        //    Debug.Log(probe.hit);
        //}
        if(Input.GetKeyDown("space")){
            StartGeneration();
        }
        if(Input.GetKeyDown("r")){
            buildWalls();
        }
        
    }
}
