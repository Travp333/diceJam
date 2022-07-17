using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    SceneController scene;
    [SerializeField]
    GameObject[] monsters;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
            if(g.GetComponent<SceneController>() != null){
                scene = g.GetComponent<SceneController>();
            }
        }
        foreach(GameObject g in monsters){
            g.SetActive(false);
        }
    }

    public void startBattle(GameObject enemy){
        scene.FreezePlayer();
        if (enemy.gameObject.tag == "turtBase"){
            monsters[3].SetActive(true);
        }
        if (enemy.gameObject.tag == "turtAlt"){
            monsters[4].SetActive(true);
        }
        if (enemy.gameObject.tag == "turtAlt2"){
            monsters[5].SetActive(true);
        }
        if (enemy.gameObject.tag == "fellaBase"){
            monsters[0].SetActive(true);
        }
        if (enemy.gameObject.tag == "fellaAlt"){
            monsters[1].SetActive(true);
        }
        if (enemy.gameObject.tag == "fellaAlt2"){
            monsters[2].SetActive(true);
        }
    }
    public void endBattle(){
        scene.UnFreezePlayer();
        foreach(GameObject g in monsters){
            g.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
