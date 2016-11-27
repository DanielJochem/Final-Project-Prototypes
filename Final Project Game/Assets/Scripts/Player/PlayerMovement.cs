using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    public TurnHandler turnHandler;

    [HideInInspector]
    public int xTilesAmount;

    public int currentTileNumber, wantedTileNumber;

    [SerializeField]
    private bool moveDelay;

    private float timeDelay;

    public  string direction = "";


    void Start() {
        timeDelay = turnHandler.timeDelay;
    }


    public void PlayerMovementLogic() {
        if(currentTileNumber == wantedTileNumber) {
            wantedTileNumber = -1;
            //Debug.Log("The Same");
            direction = "";
        }

        if(wantedTileNumber != -1) {
            if(!moveDelay) {
                if(currentTileNumber != wantedTileNumber) {
                    Move();
                    //Debug.Log("Player Moved");
                    moveDelay = true;
                } else {
                    wantedTileNumber = -1;
                }
            }

            if(direction != "") {
                if(timeDelay > 0.0f) {
                    timeDelay -= Time.deltaTime;
                } else {
                    moveDelay = false;

                    turnHandler.turnNumber++;
                    timeDelay = turnHandler.timeDelay;
                }
            } else {
                moveDelay = false;
            }
        } else {
            timeDelay = turnHandler.timeDelay;
        }
    }


    public void Move() {
        if(direction == "") {
            //Up
            if(wantedTileNumber < currentTileNumber && ((currentTileNumber - wantedTileNumber) % xTilesAmount == 0)) {
                //Debug.Log("Up");
                direction = "Up";

            //Down
            } else if(wantedTileNumber > currentTileNumber && ((wantedTileNumber - currentTileNumber) % xTilesAmount == 0)) {
                //Debug.Log("Down");
                direction = "Down";

            //Left
            } else if(wantedTileNumber < currentTileNumber && ((Mathf.CeilToInt((currentTileNumber - 1) / xTilesAmount)) == (Mathf.CeilToInt(wantedTileNumber / xTilesAmount))) && !(wantedTileNumber % xTilesAmount == 0)) {
                //Debug.Log("Left");
                direction = "Left";

            //Right
            } else if(wantedTileNumber > currentTileNumber && ((Mathf.CeilToInt(currentTileNumber / xTilesAmount)) == (Mathf.CeilToInt((wantedTileNumber - 1) / xTilesAmount))) && !((wantedTileNumber - 1) % xTilesAmount == 0)) {
                //Debug.Log("Right");
                direction = "Right";
            } 

        } else {
            if(direction == "Left") {
                gameObject.transform.position += new Vector3(-1, 0, 0);
                currentTileNumber -= 1;
            } else if(direction == "Right") {
                gameObject.transform.position += new Vector3(1, 0, 0);
                currentTileNumber += 1;
            } else if(direction == "Up") {
                gameObject.transform.position += new Vector3(0, 1, 0);
                currentTileNumber -= xTilesAmount;
            } else if(direction == "Down") {
                gameObject.transform.position += new Vector3(0, -1, 0);
                currentTileNumber += xTilesAmount;
            }
        }
    }
}