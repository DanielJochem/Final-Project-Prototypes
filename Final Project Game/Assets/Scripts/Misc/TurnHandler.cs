using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {

    public PlayerMovement player;

    public float timeDelay;
    public int turnNumber;
    private int turnNumberSAVED;

    [HideInInspector]
    public bool levelSet;

    [HideInInspector]
    public List<GameObject> enemyList;


    void Update() {
        if(levelSet) {
            player.PlayerMovementLogic();

            if(turnNumber > turnNumberSAVED) {
                foreach(GameObject enemy in enemyList) {
                    enemy.GetComponent<EnemyMovement>().EnemyMovementLogic();
                }

                turnNumberSAVED++;
            }
        }
    }
}
