using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {

    public Player player;
    public TilePlacer tilePlacer;

    public float timeDelay;
    public int turnNumber;
    private int turnNumberSAVED;

    public GameObject wonGameUI, lostGameUI;

    [HideInInspector]
    public bool levelSet, playerAttackInsteadOfMove;

    [HideInInspector]
    public List<GameObject> enemyList;

    [HideInInspector]
    public GameObject enemyToAttack;


    void Update() {
        if(levelSet) {
            if(playerAttackInsteadOfMove) {
                player.Attack(enemyToAttack);
                if(enemyList.Count == 0) {
                    wonGameUI.SetActive(true);
                }
            } else {
                player.Movement();
            }

            if(turnNumber > turnNumberSAVED && enemyList.Count > 0) {
                foreach(GameObject enemy in enemyList) {
                    enemy.GetComponent<EnemyMovement>().EnemyMovementLogic();
                }

                turnNumberSAVED++;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }

    public void RestartGame() {
        wonGameUI.SetActive(false);
        lostGameUI.SetActive(false);
        turnNumber = turnNumberSAVED = 0;
        player.health.health = player.health.maxHealth;
        player.health.healthBar.value = player.health.maxHealth;
        player.movement.direction = "";
        tilePlacer.RestartGame();
    }

    public void QuitGame() {
        lostGameUI.SetActive(false);
        Application.Quit();
    }
}
