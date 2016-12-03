using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    private TurnHandler turnHandler;

    [SerializeField]
    private PlayerMovement player;

    [HideInInspector]
    public int xTilesAmount, yTilesAmount;

    [Space(20)]
    public int currentTileNumber;

    public List<string> illegalMoves, acceptableMoves;

    [SerializeField]
    private List<string> directions;

    private bool attackThisTurn;

    private int turn = 1;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        player = FindObjectOfType<PlayerMovement>();
    }


    public void EnemyMovementLogic() {
        PlayerAndEnemyLocationChecker();

        if(attackThisTurn) {
            //Don't move, instead attack!


            attackThisTurn = false;

        } else {
            //Continue checking for movement directions
            CheckBoundaries();

            //All checks are done, now the Enemy can move!
            MovementCheckerAndMove();
        }
        
        illegalMoves.Clear();
        acceptableMoves.Clear();
    }


    void PlayerAndEnemyLocationChecker() {
        //Up
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber - xTilesAmount) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - xTilesAmount) {
                    illegalMoves.Add("Up");
                }
            }
        } else {
            //Attack, don't move
            print("Attack Up" + turn);
            attackThisTurn = true;
        }

        //UpRight
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber - (xTilesAmount - 1)) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - (xTilesAmount - 1)) {
                    illegalMoves.Add("UpRight");
                }
            }
        } else {
            //Attack, don't move
            print("Attack UpRight" + turn);
            attackThisTurn = true;
        }

        //Right
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber + 1) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + 1) {
                    illegalMoves.Add("Right");
                }
            }
        } else {
            //Attack, don't move
            print("Attack Right" + turn);
            attackThisTurn = true;
        }

        //DownRight
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber + (xTilesAmount + 1)) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + (xTilesAmount + 1)) {
                    illegalMoves.Add("DownRight");
                }
            }
        } else {
            //Attack, don't move
            print("Attack DownRight" + turn);
            attackThisTurn = true;
        }

        //Down
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber + xTilesAmount) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + xTilesAmount) {
                    illegalMoves.Add("Down");
                }
            }
        } else {
            //Attack, don't move
            print("Attack Down" + turn);
            attackThisTurn = true;
        }

        //DownLeft
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber + (xTilesAmount - 1)) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber + (xTilesAmount - 1)) {
                    illegalMoves.Add("DownLeft");
                }
            }
        } else {
            //Attack, don't move
            print("Attack DownLeft" + turn);
            attackThisTurn = true;
        }

        //Left
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber - 1) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - 1) {
                    illegalMoves.Add("Left");
                }
            }
        } else {
            //Attack, don't move
            print("Attack Left" + turn);
            attackThisTurn = true;
        }

        //UpLeft
        //If Player is not in wantedPosition
        if(player.currentTileNumber != currentTileNumber - (xTilesAmount + 1)) {
            //If no Enemy is in that position
            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == currentTileNumber - (xTilesAmount + 1)) {
                    illegalMoves.Add("UpLeft");
                }
            }
        } else {
            //Attack, don't move
            print("Attack UpLeft" + turn);
            attackThisTurn = true;
        }

        turn++;
    }


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

        //string[] tempArray = acceptableMoves.ToArray();
        //string tempString = string.Join(", ", tempArray);
        //print("List of Available Moves: " + tempString);
        //print("Direction Chosen: " + acceptableMoves[directionChoice]);
        return acceptableMoves[directionChoice];
    }


    void MovementCheckerAndMove() {
        string moveDirection = "";

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

        if(acceptableMoves.Count > 0) {
            moveDirection = RandomDirection();
        }

        if(moveDirection != "") {
            Move(moveDirection);
        }
    }


    void Move(string direction) {
        //print("Enemy Moved");

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
    }
}