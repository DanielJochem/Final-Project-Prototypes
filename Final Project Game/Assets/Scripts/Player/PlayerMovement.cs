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
        //timeDelay = turnHandler.timeDelay;
        //armourType = ArmourType.leather;
        armourTypeValue = (int)armourType;
        //Debug.Log("The current armour worn is: " + System.Enum.GetName(typeof(ArmourType), armourType));
    }


    public void PlayerMovementLogic() {
        //If the tile currently on is the wantedTile, reset the wantedTileNumber and the direction.
        if(currentTileNumber == wantedTileNumber) {
            wantedTileNumber = -1;
            direction = "";
        }

        //If the Player has clicked on a tile they want to move to,
        if(wantedTileNumber != -1) {
            /***** NOTE: The stuff commented out with '/*' is for if we want to have delayed movement when moving two tiles in one turn (when wearing leather armour).*****/
             
            //If the time delay between moves counted down to 0,
            /*if(!moveDelay) {*/
                //If the Player is not currently on the wantedTile,
                if(currentTileNumber != wantedTileNumber) {
                    //Move.
                    Move();
                    /*moveDelay = true;*/
                } else {
                    wantedTileNumber = -1;
                }
            /*}*/

            /*if(direction != "") {
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
            timeDelay = turnHandler.timeDelay; */
        }
    }


    public void Move() {
        //If no current direction has been determined.
        if(direction == "") {
            //Up                                                                          //Player can move two in one turn but they choose to move only one.
            if(wantedTileNumber == (currentTileNumber - (xTilesAmount * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - xTilesAmount)) /*&& ((currentTileNumber - wantedTileNumber) % xTilesAmount == 0)*/) {
                //Debug.Log("Up");
                direction = "Up";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - xTilesAmount)) {
                    canMoveTwoChoseToMoveOne = true;
                }

            //Left                                                                     //Player can move two in one turn but they choose to move only one.
            } else if((wantedTileNumber == (currentTileNumber - (1 * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - 1))) && (Mathf.CeilToInt((float)currentTileNumber / (float)xTilesAmount) == (Mathf.CeilToInt((float)wantedTileNumber / (float)xTilesAmount))) /*&& !(wantedTileNumber % xTilesAmount == 0)*/) {
                //Debug.Log("Left");
                direction = "Left";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - 1)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //Right                                                                //Player can move two in one turn but they choose to move only one.
            } else if((wantedTileNumber == (currentTileNumber + (1 * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + 1))) && (Mathf.CeilToInt((float)currentTileNumber / (float)xTilesAmount) == (Mathf.CeilToInt((float)wantedTileNumber / (float)xTilesAmount))) /*&& !((wantedTileNumber - 1.0f) % xTilesAmount == 0)*/) {
                //Debug.Log("Right");
                direction = "Right";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + 1)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //Down                                                                           //Player can move two in one turn but they choose to move only one.
            } else if(wantedTileNumber == (currentTileNumber + (xTilesAmount * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + xTilesAmount)) /*&& ((wantedTileNumber - currentTileNumber) % xTilesAmount == 0)*/) {
                //Debug.Log("Down");
                direction = "Down";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + xTilesAmount)) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //DownRight                                                                            //Player can move two in one turn but they choose to move only one.                                                                                                                                                                                                                           //<-- Up to this point uncommented, the problem is with DR and UL 3        //<-- Up to this point uncommented, the problem is with DL and UR 4        //The rest uncommented, the problem is with DR and UL 4 (I think)
            } else if(wantedTileNumber == (currentTileNumber + ((xTilesAmount + 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount + 1))) /*&& ((wantedTileNumber - currentTileNumber) % (xTilesAmount + 1) == 0) && (wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4))*/) {                    // || wantedTileNumber - currentTileNumber != ((xTilesAmount + 1) * 4))) { //&& wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4) || wantedTileNumber - currentTileNumber != ((xTilesAmount - 1) * 4)) {
                //Debug.Log("DownRight");
                direction = "DownRight";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount + 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //DownLeft                                                                             //Player can move two in one turn but they choose to move only one.
            } else if(wantedTileNumber == (currentTileNumber + ((xTilesAmount - 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount - 1))) /*&& ((wantedTileNumber - currentTileNumber) % (xTilesAmount - 1) == 0)*/) {
                //Debug.Log("DownLeft");
                direction = "DownLeft";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber + (xTilesAmount - 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //UpLeft                                                                               //Player can move two in one turn but they choose to move only one.                                                                                                                                                                                            //<-- Up to this point uncommented, the problem is with DR and UL 3        //<-- Up to this point uncommented, the problem is with DL and UR 4        //The rest uncommented, the problem is with DR and UL 4 (I think)
            } else if(wantedTileNumber == (currentTileNumber - ((xTilesAmount + 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount + 1))) /*&& ((currentTileNumber - wantedTileNumber) % (xTilesAmount + 1) == 0) && (currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 3))*/) {                    // || currentTileNumber - wantedTileNumber != ((xTilesAmount - 1) * 3))) { //&& currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 3) || currentTileNumber - wantedTileNumber != ((xTilesAmount + 1) * 4)) {
                //Debug.Log("UpLeft");
                direction = "UpLeft";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount + 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

                //UpRight                                                                              //Player can move two in one turn but they choose to move only one.
            } else if(wantedTileNumber == (currentTileNumber - ((xTilesAmount - 1) * armourTypeValue)) || (armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount - 1))) /*&& ((currentTileNumber - wantedTileNumber) % (xTilesAmount - 1) == 0)*/) {
                //Debug.Log("UpRight");
                direction = "UpRight";

                //Chose to move only one tile with leather armour on.
                if(armourType == ArmourType.leather && wantedTileNumber == (currentTileNumber - (xTilesAmount - 1))) {
                    canMoveTwoChoseToMoveOne = true;
                }

            } else {
                wantedTileNumber = currentTileNumber;
                direction = "";
            }
        }


        //This is the logic to actually move in the chosen direction.
        if(direction == "Up") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= xTilesAmount;
                gameObject.transform.position += new Vector3(0, 1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber -= xTilesAmount * armourTypeValue;
                gameObject.transform.position += new Vector3(0, 1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "UpRight") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= xTilesAmount - 1;
                gameObject.transform.position += new Vector3(1, 1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber -= (xTilesAmount - 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, 1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "Right") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += 1;
                gameObject.transform.position += new Vector3(1, 0, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber += 1 * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, 0, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "DownRight") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += (xTilesAmount + 1);
                gameObject.transform.position += new Vector3(1, -1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber += (xTilesAmount + 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(1 * armourTypeValue, -1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "Down") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += xTilesAmount;
                gameObject.transform.position += new Vector3(0, -1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber += xTilesAmount * armourTypeValue;
                gameObject.transform.position += new Vector3(0, -1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "DownLeft") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber += (xTilesAmount - 1);
                gameObject.transform.position += new Vector3(-1, -1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber += (xTilesAmount - 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, -1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "Left") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= 1;
                gameObject.transform.position += new Vector3(-1, 0, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber -= 1 * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, 0, 0);
            }

            turnHandler.turnNumber++;

        } else if(direction == "UpLeft") {
            //If chose to move only one tile with leather armour on.
            if(canMoveTwoChoseToMoveOne) {
                currentTileNumber -= (xTilesAmount + 1);
                gameObject.transform.position += new Vector3(-1, 1, 0);
                canMoveTwoChoseToMoveOne = false;

            } else { //Move the amount the currently worn armour type allows you to move.
                currentTileNumber -= (xTilesAmount + 1) * armourTypeValue;
                gameObject.transform.position += new Vector3(-1 * armourTypeValue, 1 * armourTypeValue, 0);
            }

            turnHandler.turnNumber++;
        }
    }


    //If the armour type is changed, this is a nice little method to call.
    int ChangeArmourTypeTo(ArmourType armour) {
        return (int)armour;
    }
}