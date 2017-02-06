using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {

    public Player player;
    public TilePlacer tilePlacer;

    //For if we want delayed movement when moving 2 tiles in one turn with leather armour.
    //public float timeDelay;

    public int turnNumber;
    private int turnNumberSAVED;

    public GameObject wonGameUI, lostGameUI;

    [HideInInspector]
    public bool levelSet, playerAttackInsteadOfMove, gameRestarted, movementInProgress, wasEnemyOnTile;

    [HideInInspector]
    public List<GameObject> enemyList;

    [HideInInspector]
    public GameObject enemyToAttack;


    void Update() {
        //If all tiles have been placed, Player has been placed, and all enemies have been placed,
        if(levelSet) {
            //If there was no enemy on the tile clicked on, we want to move, not attack.
            if(!playerAttackInsteadOfMove && player.movement.wantedTileNumber != -1 && !movementInProgress && !wasEnemyOnTile) {
                //The four references to this 'movementInProgress' thing in this script (it is the only script that has it) is to limit the amount of times player.Movement() is tried.
                movementInProgress = true;
                player.Movement();
                movementInProgress = false;
            }

            //Turn logic for enemies.
            if(turnNumber > turnNumberSAVED && enemyList.Count > 0) {
                foreach(GameObject enemy in enemyList) {
                    enemy.GetComponent<EnemyMovement>().EnemyTurnLogic();
                }

                //Once all the enemies alive have moved and attacked, the current turn is over. Onto the next turn!
                turnNumberSAVED++;
            }
        }

        //Exit the game if you press the Escape key or the back button on an Android device.
        if(Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }


    public void TilesBackToPurple() {
        foreach(GameObject tile in tilePlacer.tiles) {
            if(tile.GetComponent<SpriteRenderer>().color == Color.red) {
                tile.GetComponent<SpriteRenderer>().color = new Color(0.5568f, 0.0156f, 0.8902f);
            }
        }
    }


    //Player chose to restart the game (because they lost) or to move onto the next level, so reset everything.
    public void RestartGame() {
        wonGameUI.SetActive(false);
        lostGameUI.SetActive(false);
        turnNumber = turnNumberSAVED = 0;
        player.health.health = player.health.maxHealth;
        player.health.healthBar.value = player.health.maxHealth;
        player.movement.direction = "";
        tilePlacer.RestartGame();
    }


    //Self-explainatory.
    public void QuitGame() {
        wonGameUI.SetActive(false);
        lostGameUI.SetActive(false);
        Application.Quit();
    }
}
