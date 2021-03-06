﻿using UnityEngine;

public class Tile : MonoBehaviour {
    private Player player;
    private Enemy enemyRef;
    private TurnHandler turnHandler;

    //What number am I in the tileList?
    public int listNum;

    private bool activeTimer;
    public float timer;
    private float timerSAVED;


    void Start() {
        player = FindObjectOfType<Player>();
        turnHandler = FindObjectOfType<TurnHandler>();
        enemyRef = FindObjectOfType<Enemy>();

        timerSAVED = timer;
    }


    private void Update() {
        //If the selected enemy (for displaying their attackable tiles) has changed
        if(enemyRef.enemySwapped) {
            timer = timerSAVED;
            activeTimer = true;
            enemyRef.enemySwapped = false;
        }

        if(activeTimer) {
            timer -= Time.deltaTime;
            if(enemyRef.currentSelectedEnemyIsDead || turnHandler.gameRestarted) {
                timer = 0.0f;
                activeTimer = false;
                enemyRef.currentSelectedEnemyIsDead = false;
                turnHandler.gameRestarted = false;
            }

            //Second check for activeTimer because of the above if statement, if died.
            if(timer <= 0.0f && activeTimer) {
                enemyRef.currentlySelectedEnemy.gameObject.GetComponent<EnemyAttack>().EnemyRemoveAttackableTiles();
                enemyRef.currentlySelectedEnemy = null;
                activeTimer = false;
            }
        }
    }


    //If an enemy was clicked on to see it's attackable tiles, show those tiles, if the same enemy is clicked again, attack if in range.
    public void OnMouseDown() {
        turnHandler.wasEnemyOnTile = false;
        foreach(GameObject enemy in turnHandler.enemyList) {
            if(enemy.GetComponent<EnemyMovement>().currentTileNumber == listNum) {
                turnHandler.wasEnemyOnTile = true;

                //If you clicked on a Goblin, skip all the showing of attackable tiles logic, as the Goblin doesn't have any attackable tiles. Just attack it instead.
                if(enemy.gameObject.name == "E_Goblin(Clone)") {
                    if(enemyRef.currentlySelectedEnemy != null) {
                        enemyRef.currentlySelectedEnemy.GetComponent<EnemyAttack>().EnemyRemoveAttackableTiles();
                    }

                    enemyRef.currentlySelectedEnemy = enemy;
                    AttackEnemy();
                    break;

                } else {
                    if(enemyRef.currentlySelectedEnemy != null) {
                        //If it is the same enemy as the currentlySelectedEnemy
                        if(enemy.GetComponent<EnemyMovement>().currentTileNumber == enemyRef.currentlySelectedEnemy.GetComponent<EnemyMovement>().currentTileNumber) {
                            AttackEnemy();
                            break;
                        } else {
                            //A new enemy has been selected, so remove the previously selected enemy's attackable tiles.
                            enemyRef.currentlySelectedEnemy.GetComponent<EnemyAttack>().EnemyRemoveAttackableTiles();
                        }
                    }

                    //Set the new enemy as the currentlySelectedEnemy and display it's tiles.
                    enemyRef.currentlySelectedEnemy = enemy;
                    enemy.GetComponent<EnemyAttack>().EnemyDisplayAttackableTiles();

                    enemyRef.enemySwapped = true;

                    //Reset the currentlySelectedEnemy timer.
                    timer = timerSAVED;
                    break;
                }
            }
        } 
    }


    //When tile is clicked on,
    public void OnMouseUp() {
        //If there was no enemy on the clicked tile,
        if(!turnHandler.playerAttackInsteadOfMove && !turnHandler.wasEnemyOnTile) {
            if(player.movement.direction == "") {
                //Debug.Log("I am List Number: " + listNum + ". My Position is: " + gameObject.transform.GetChild(0).GetChild(0).transform.position);
                //Set the clicked tile as the wantedTile.
                player.movement.wantedTileNumber = listNum;
            }
        } else {
            //Some cleanup
            turnHandler.wasEnemyOnTile = false;
        }
    }


    public void AttackEnemy() {
        //Make sure we can't click ANY enemy on the board to attack (this long IF statement will only allow for closest tile in all 8 directions). Need to change for longer range attacks.
        if((player.movement.currentTileNumber - player.movement.xTilesAmount) == listNum                //Up
            || (player.movement.currentTileNumber - (player.movement.xTilesAmount - 1)) == listNum      //UpRight
            || (player.movement.currentTileNumber + 1) == listNum                                       //Right
            || (player.movement.currentTileNumber + (player.movement.xTilesAmount + 1)) == listNum      //DownRight
            || (player.movement.currentTileNumber + player.movement.xTilesAmount) == listNum            //Down
            || (player.movement.currentTileNumber + (player.movement.xTilesAmount - 1)) == listNum      //DownLeft
            || (player.movement.currentTileNumber - 1) == listNum                                       //Left
            || (player.movement.currentTileNumber - (player.movement.xTilesAmount + 1)) == listNum) {   //UpLeft

            //If there is an enemy on the tile we are clicking on, target the enemy and set the player mode to Attack instead of Move.
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == listNum) {
                    turnHandler.enemyToAttack = enemy;
                    turnHandler.playerAttackInsteadOfMove = true;
                    break;
                }
            }

            //Attack
            player.Attack(enemyRef.currentlySelectedEnemy);
        }
    }
}
