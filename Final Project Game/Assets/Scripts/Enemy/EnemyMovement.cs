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

    private List<string> illegalMoves, acceptableMoves;

    [SerializeField]
    private List<string> directions;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        player = FindObjectOfType<PlayerMovement>();
    }


    public void EnemyMovementLogic() {
        PlayerAndEnemyLocationChecker();
        CheckBoundaries();

        //All checks are done, now the Enemy can move!
        MovementCheckerAndMove();
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
            illegalMoves.Add("Up");
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
            illegalMoves.Add("UpRight");
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
            illegalMoves.Add("Right");
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
            illegalMoves.Add("DownRight");
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
            illegalMoves.Add("Down");
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
            illegalMoves.Add("DownLeft");
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
            illegalMoves.Add("Left");
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
            illegalMoves.Add("UpLeft");
        }
    }


    void CheckBoundaries() {
        //Up
        if(currentTileNumber - xTilesAmount <= 0) {
            illegalMoves.Add("Up");
        }

        //UpRight
        if(currentTileNumber - (xTilesAmount - 1) < 2) {
            illegalMoves.Add("UpRight");
        }

        //Right
        if(currentTileNumber % xTilesAmount == 0) {
            illegalMoves.Add("Right");
        }

        //DownRight
        if(currentTileNumber + (xTilesAmount + 1) > xTilesAmount * yTilesAmount) {
            illegalMoves.Add("DownRight");
        }

        //Down
        if(currentTileNumber + xTilesAmount > xTilesAmount * yTilesAmount) {
            illegalMoves.Add("Down");
        }

        //DownLeft
        if(currentTileNumber + (xTilesAmount - 1) > (xTilesAmount * yTilesAmount) - 1) {
            illegalMoves.Add("DownLeft");
        }

        //Left
        if(currentTileNumber % xTilesAmount == 1) {
            illegalMoves.Add("Left");
        }

        //UpLeft
        if(currentTileNumber - (xTilesAmount + 1) <= 0) {
            illegalMoves.Add("UpLeft");
        }
    }


    string RandomDirection() {
        int directionChoice = Random.Range(0, acceptableMoves.Count);
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

        illegalMoves.Clear();
        acceptableMoves.Clear();
    }


    void Move(string direction) {
        //Debug.Log("Enemy Moved");

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