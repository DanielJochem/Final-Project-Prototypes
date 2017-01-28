using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    private TurnHandler turnHandler;

    #region Armour Fields
    public enum ArmourType {
        plate = 1,
        leather
    }

    public ArmourType armourType;

    private int armourTypeValue;
    #endregion

    #region Misc Fields
    [HideInInspector]
    public int xTilesAmount;

    //[HideInInspector]
    public int currentTileNumber = -1, wantedTileNumber = -1;
    
    private bool moveDelay;
    
    private float timeDelay;

    [HideInInspector]
    public string direction = "";

    private bool canMoveTwoChoseToMoveOne;
    #endregion

    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        timeDelay = turnHandler.timeDelay;
        //armourType = ArmourType.leather;
        armourTypeValue = (int)armourType;
        //Debug.Log("The current armour worn is: " + System.Enum.GetName(typeof(ArmourType), armourType));
    }


    public void PlayerMovementLogic() {
        if(currentTileNumber == wantedTileNumber) {
            wantedTileNumber = -1;
            direction = "";
        }

        if(wantedTileNumber != -1) {
            if(!moveDelay) {
                if(currentTileNumber != wantedTileNumber) {
                    Move();
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
            //Up                                                                           //All these OR conditions are to check if can move two in one turn but choose to move one.
            if(wantedTileNumber == (currentTileNumber - (xTilesAmount * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - xTilesAmount)) /*&& ((currentTileNumber - wantedTileNumber) % xTilesAmount == 0)*/) {
                //Debug.Log("Up");
                direction = "Up";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - xTilesAmount)) {
                    canMoveTwoChoseToMoveOne = true;
                }

            //Left
            } else if((wantedTileNumber == (currentTileNumber - (1 * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - 1))) && (Mathf.CeilToInt((float)currentTileNumber / (float)xTilesAmount) == (Mathf.CeilToInt((float)wantedTileNumber / (float)xTilesAmount))) /*&& !(wantedTileNumber % xTilesAmount == 0)*/) {
                //Debug.Log("Left");
                direction = "Left";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - 1)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //Right
            } else if((wantedTileNumber == (currentTileNumber + (1 * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + 1))) && (Mathf.CeilToInt((float)currentTileNumber / (float)xTilesAmount) == (Mathf.CeilToInt((float)wantedTileNumber / (float)xTilesAmount))) /*&& !((wantedTileNumber - 1.0f) % xTilesAmount == 0)*/) {
                //Debug.Log("Right");
                direction = "Right";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + 1)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //Down
            } else if(wantedTileNumber == (currentTileNumber + (xTilesAmount * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + xTilesAmount)) /*&& ((wantedTileNumber - currentTileNumber) % xTilesAmount == 0)*/) {
                //Debug.Log("Down");
                direction = "Down";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + xTilesAmount)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //DownRight                                                                                                                                                                                                                                             //<-- Up to this point uncommented, the problem is with DR and UL 3        //<-- Up to this point uncommented, the problem is with DL and UR 4        //The rest uncommented, the problem is with DR and UL 4 (I think)
            } else if(wantedTileNumber == (currentTileNumber + ((xTilesAmount + 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount + 1))) /*&& ((wantedTileNumber - currentTileNumber) % (xTilesAmount + 1) == 0) && (wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4))*/) {                    // || wantedTileNumber - currentTileNumber != ((xTilesAmount + 1) * 4))) { //&& wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4) || wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4)) {
                //Debug.Log("DownRight");
                direction = "DownRight";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount + 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //DownLeft
            } else if(wantedTileNumber == (currentTileNumber + ((xTilesAmount - 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount - 1))) /*&& ((wantedTileNumber - currentTileNumber) % (xTilesAmount - 1) == 0)*/) {
                //Debug.Log("DownLeft");
                direction = "DownLeft";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount - 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //UpLeft                                                                                                                                                                                                     //<-- Up to this point uncommented, the problem is with DR and UL 3        //<-- Up to this point uncommented, the problem is with DL and UR 4        //The rest uncommented, the problem is with DR and UL 4 (I think)
            } else if(wantedTileNumber == (currentTileNumber - ((xTilesAmount + 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount + 1))) /*&& ((currentTileNumber - wantedTileNumber) % (xTilesAmount + 1) == 0) && (currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 3))*/) {                    // || currentTileNumber - wantedTileNumber != ((xTilesAmount - 1) * 3))) { //&& currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 3) || currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 4)) {
                //Debug.Log("UpLeft");
                direction = "UpLeft";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount + 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //UpRight
            } else if(wantedTileNumber == (currentTileNumber - ((xTilesAmount - 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount - 1))) /*&& ((currentTileNumber - wantedTileNumber) % (xTilesAmount - 1) == 0)*/) {
                //Debug.Log("UpRight");
                direction = "UpRight";
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount - 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

            } else {
                wantedTileNumber = currentTileNumber;
                direction = "";
            }
        }


        if(direction == "Up") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= xTilesAmount;
                gameObject.transform.position += new Vector3(0, 1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber -= xTilesAmount * armourTypeValue;
                gameObject.transform.position += new Vector3(0, 1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "UpRight") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= xTilesAmount - 1;
                gameObject.transform.position += new Vector3(1, 1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber -= (xTilesAmount - 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, 1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "Right") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += 1;
                gameObject.transform.position += new Vector3(1, 0, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber += 1 * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, 0, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "DownRight") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += (xTilesAmount + 1);
                gameObject.transform.position += new Vector3(1, -1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber += (xTilesAmount + 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, -1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "Down") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += xTilesAmount;
                gameObject.transform.position += new Vector3(0, -1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber += xTilesAmount * armourTypeValue;
                gameObject.transform.position += new Vector3(0, -1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "DownLeft") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += (xTilesAmount - 1);
                gameObject.transform.position += new Vector3(-1, -1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber += (xTilesAmount - 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, -1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "Left") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= 1;
                gameObject.transform.position += new Vector3(-1, 0, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber -= 1 * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, 0, 0);
            }
            turnHandler.turnNumber++;

        } else if(direction == "UpLeft") {
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= (xTilesAmount + 1);
                gameObject.transform.position += new Vector3(-1, 1, 0);
                canMoveTwoChoseToMoveOne = false;
            } else {
                currentTileNumber -= (xTilesAmount + 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, 1 * armourTypeValue, 0);
            }
            turnHandler.turnNumber++;
        }
    }

    int ChangeArmourTypeTo(ArmourType armour) {
        return (int)armour;
    }
}