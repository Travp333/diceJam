using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonSizeController : MonoBehaviour
{
    [SerializeField]
    public int chunkCount;
    [SerializeField]
    public int maxChunks;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void resetChunkCount(){
        chunkCount = 0;
    }
    public void incrementChunkCount(){
        chunkCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
