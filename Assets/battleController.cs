using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    Hand hand2;
    Hand hand;
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
            if(g.GetComponent<Hand>() != null && g.GetComponent<Hand>().isPlayer){
                hand = g.GetComponent<Hand>();
            }
        }
        foreach(GameObject g in monsters){
            g.SetActive(false);
        }
    }

    public GameObject whichMonsterBattling(GameObject enemy){
        if (enemy.gameObject.tag == "turtBase"){
           return monsters[3];
        }
        if (enemy.gameObject.tag == "turtAlt"){
            return monsters[4];
        }
        if (enemy.gameObject.tag == "turtAlt2"){
            return monsters[5];
        }
        if (enemy.gameObject.tag == "fellaBase"){
            return monsters[0];
        }
        if (enemy.gameObject.tag == "fellaAlt"){
            return monsters[1];
        }
        if (enemy.gameObject.tag == "fellaAlt2"){
            return monsters[2];
        }
        else return null;
    }

    public void startBattle(GameObject enemy){
        Debug.Log("StartBattle!");
        //transform.parent.GetComponent<fellaAnimController>().resetAll();
        scene.setCheckPoint(enemy);
        scene.battleScene = true;
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
        hand.updateCheckReference(whichMonsterBattling(enemy).GetComponent<diceCheckpoint>());
        hand2 = whichMonsterBattling(enemy).GetComponent<Hand>();
        if(whichMonsterBattling(enemy).GetComponent<fellaAnimController>() != null){
            whichMonsterBattling(enemy).GetComponent<fellaAnimController>().setthrowinDie();
        }
        if(whichMonsterBattling(enemy).GetComponent<diceCheckpoint>() != null){
            whichMonsterBattling(enemy).GetComponent<diceCheckpoint>().resetHP();
        }
    }
    public void endBattle(){
        
        scene.ClearMessage();
        scene.battleScene = false;
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
