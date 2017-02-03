using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {
    private TurnHandler turnHandler;
    private Player player;
    private Enemy enemy;

    [HideInInspector] //References to the horizontal and vertical tile amounts.
    public int xTilesAmount, yTilesAmount;
    
    //The tile the enemy spawns on.
    public int currentTileNumber;

    //For checking what moves are vallid and invalid each turn.
    public List<string> illegalMoves, acceptableMoves;

    [SerializeField] //The 8 total directions.
    private List<string> directions;

    //For checking if player is in a tile the enemy can attack in.
    private bool attackThisTurn;


    void Start() {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();

        turnHandler = enemy.turnHandler;
        xTilesAmount = enemy.xTilesAmount;
        yTilesAmount = enemy.yTilesAmount;
    }


    public void EnemyTurnLogic() {
        //Check for the player's and all other enemy's current tile. This prevents enemies stacking on top of each other and/or the player.
        PlayerAndEnemyLocationChecker();
        attackThisTurn = gameObject.GetComponent<EnemyAttack>().CheckCanAttackPlayer();

        //If player is in a tile the enemy can attack in,
        if(attackThisTurn) {
            //Don't move, instead attack!
            enemy.Attack();
            attackThisTurn = false;

        } else {
            //Continue checking for movement directions
            CheckBoundaries();

            //All checks are done, now the Enemy can pick a direction to move in!
            MovementCheckerAndMove();
        }
        
        illegalMoves.Clear();
        acceptableMoves.Clear();
    }


    //Checks to see if the Player or any other enemies are in the tile the current enemy wants to move to.
    void PlayerAndEnemyLocationChecker() {
        //Up
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber - xTilesAmount) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - xTilesAmount) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("Up");
                }
            }
        } else { //Player is in the current enemy's wantedPosition,
            illegalMoves.Add("Up");
        }

        //UpRight
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber - (xTilesAmount - 1)) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - (xTilesAmount - 1)) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("UpRight");
                }
            }
        } else {
            illegalMoves.Add("UpRight");
        }

        //Right
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber + 1) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + 1) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("Right");
                }
            }
        } else {
            illegalMoves.Add("Right");
        }

        //DownRight
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber + (xTilesAmount + 1)) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + (xTilesAmount + 1)) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("DownRight");
                }
            }
        } else {
            illegalMoves.Add("DownRight");
        }

        //Down
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber + xTilesAmount) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + xTilesAmount) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("Down");
                }
            }
        } else {
            illegalMoves.Add("Down");
        }

        //DownLeft
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber + (xTilesAmount - 1)) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + (xTilesAmount - 1)) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("DownLeft");
                }
            }
        } else {
            illegalMoves.Add("DownLeft");
        }

        //Left
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber - 1) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - 1) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("Left");
                }
            }
        } else {
            illegalMoves.Add("Left");
        }

        //UpLeft
        //If Player is not in the enemy's wantedPosition,
        if(player.movement.currentTileNumber != currentTileNumber - (xTilesAmount + 1)) {
            //Check all other enemy's positions,
            foreach(GameObject enemy in turnHandler.enemyList) {
                //If another enemy is in that position,
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - (xTilesAmount + 1)) {
                    //It is an illegal movement (no stacking enemies on top of each other).
                    illegalMoves.Add("UpLeft");
                }
            }
        } else {
            illegalMoves.Add("UpLeft");
        }


        //Final Check for Top-Left tile (It is a very gross tile, never want to move there.
        if(currentTileNumber - xTilesAmount == 1) {
            illegalMoves.Add("Up");
        } else if(currentTileNumber - (xTilesAmount - 1) == 1) {
            illegalMoves.Add("UpLeft");
        } else if (currentTileNumber - 1 == 1) {
            illegalMoves.Add("Left");
        }
    }


    //We do not want enemies to go outside the boundaries of the map, so make moves illegal if they would put the enemy outside the tiles.
    void CheckBoundaries() {
        //Up
        if(currentTileNumber - xTilesAmount <= 0) {
            illegalMoves.Add("Up");
        }

        //UpRight
        if(currentTileNumber - (xTilesAmount - 1) < 2 || (currentTileNumber - (xTilesAmount - 1)) % xTilesAmount == 1) {
            illegalMoves.Add("UpRight");
        }

        //Right
        if(currentTileNumber % xTilesAmount == 0) {
            illegalMoves.Add("Right");
        }

        //DownRight
        if(currentTileNumber + (xTilesAmount + 1) > xTilesAmount * yTilesAmount || (currentTileNumber + (xTilesAmount + 1)) % xTilesAmount == 1) {
            illegalMoves.Add("DownRight");
        }

        //Down
        if(currentTileNumber + xTilesAmount > xTilesAmount * yTilesAmount) {
            illegalMoves.Add("Down");
        }

        //DownLeft
        if(currentTileNumber + (xTilesAmount - 1) > (xTilesAmount * yTilesAmount) - 1 || (currentTileNumber + (xTilesAmount - 1)) % xTilesAmount == 0) {
            illegalMoves.Add("DownLeft");
        }

        //Left
        if(currentTileNumber % xTilesAmount == 1) {
            illegalMoves.Add("Left");
        }

        //UpLeft
        if(currentTileNumber - (xTilesAmount + 1) <= 0 || (currentTileNumber - (xTilesAmount + 1)) % xTilesAmount == 0) {
            illegalMoves.Add("UpLeft");
        }
    }


    string RandomDirection() {
        int directionChoice = Random.Range(0, acceptableMoves.Count);

        //DEBUG STUFF
        //string[] tempArray = acceptableMoves.ToArray();
        //string tempString = string.Join(", ", tempArray);
        //print("List of Available Moves: " + tempString);
        //print("Direction Chosen: " + acceptableMoves[directionChoice]);
        return acceptableMoves[directionChoice];
    }


    void MovementCheckerAndMove() {
        string moveDirection = "";

        //This is slight fsckery, but it basically checks all possible moves against the illegalMoves
        //and if it doesn't find the direction it is currently checking from the list of all possibe directions in the illegalMoves list,
        //it must be an acceptable movement, so add it to the acceptableMoves list.
        foreach(string directionToCheck in directions) {
            bool directionFound = false;
            foreach(string badDirection in illegalMoves) {
                if(badDirection == directionToCheck) {
                    directionFound = true;
                    break;
                }
            }

            if(!directionFound) {
                acceptableMoves.Add(directionToCheck);
            }
        }

        //If after that, the acceptableMoves list has at least 1 item (direction),
        if(acceptableMoves.Count > 0) {
            //Pick a random direction from the list.
            moveDirection = RandomDirection();
        }

        //If the acceptableMoves list had at least 1 item (direction) in it, it chose a random direction from thelist to move in,
        if(moveDirection != "") {
            //Clear the previous attackeable tiles if they were visible.
            if(gameObject.GetComponent<EnemyAttack>().tilesShown) {
                gameObject.GetComponent<EnemyAttack>().EnemyRemoveAttackableTiles();
            }

            //So move in that direction.
            Move(moveDirection);
        }
    }


    //Movement logic for enemies.
    void Move(string direction) {
        //print("Enemy Moved");

        //The direction that was randomly chosen from the list of acceptableMoves.
        switch(direction) {
            case "Up":
                //Move Up
                gameObject.transform.position += new Vector3(0, 1, 0);
                currentTileNumber -= xTilesAmount;
                break;
            case "UpRight":
                //Move UpRight
                gameObject.transform.position += new Vector3(1, 1, 0);
                currentTileNumber -= (xTilesAmount - 1);
                break;
            case "Right":
                //Move Right
                gameObject.transform.position += new Vector3(1, 0, 0);
                currentTileNumber += 1;
                break;
            case "DownRight":
                //Move DownRight
                gameObject.transform.position += new Vector3(1, -1, 0);
                currentTileNumber += (xTilesAmount + 1);
                break;
            case "Down":
                //Move Down
                gameObject.transform.position += new Vector3(0, -1, 0);
                currentTileNumber += xTilesAmount;
                break;
            case "DownLeft":
                //Move DownLeft
                gameObject.transform.position += new Vector3(-1, -1, 0);
                currentTileNumber += (xTilesAmount - 1);
                break;
            case "Left":
                //Move Left
                gameObject.transform.position += new Vector3(-1, 0, 0);
                currentTileNumber -= 1;
                break;
            case "UpLeft":
                //Move UpLeft
                gameObject.transform.position += new Vector3(-1, 1, 0);
                currentTileNumber -= (xTilesAmount + 1);
                break;
        }

        //A new move has been made, which means the attackable tiles are now different, so clear the previous list of attackable tiles.
        gameObject.GetComponent<EnemyAttack>().enemyAttackableTiles.Clear();

        //Now we can calculate the new attackable tiles.
        gameObject.GetComponent<EnemyAttack>().CalculateAttackableTiles();
    }
}