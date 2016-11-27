using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public TurnHandler turnHandler;

    [HideInInspector]
    public int xTilesAmount, yTilesAmount;

    public int currentTileNumber;

    [SerializeField]
    private string lastDirection = "";


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
    }


    public void EnemyMovementLogic() {
        if(lastDirection == "") {
            //Debug.Log("Enemy Movement activated");
            if(!((currentTileNumber) % xTilesAmount == 0) && ((Mathf.CeilToInt((currentTileNumber - 1) / xTilesAmount)) == (Mathf.CeilToInt(currentTileNumber / xTilesAmount)))) {
                lastDirection = "Left";
            } else {
                lastDirection = "Right";
            }
        }

        //Checking for the 4 corners, they are hell, we never want enemies to be in them.
        if(lastDirection == "Left") {
            if(currentTileNumber + 1 == xTilesAmount || currentTileNumber + 1 == xTilesAmount * yTilesAmount) {
                lastDirection = "Right";
            }
        } else if(lastDirection == "Right") {
            if(currentTileNumber - 1 == 1 || currentTileNumber - 1 == ((xTilesAmount * yTilesAmount) - (xTilesAmount - 1))) {
                lastDirection = "Left";
            }
        }

        //Need to check if player or any other enemies are in wanted tile /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //if()
        if(lastDirection == "Left") {
            //Move Right
            gameObject.transform.position += new Vector3(1, 0, 0);
            currentTileNumber += 1;
            lastDirection = "Right";
        } else if(lastDirection == "Right") {
            //Move Left
            gameObject.transform.position += new Vector3(-1, 0, 0);
            currentTileNumber -= 1;
            lastDirection = "Left";
        }

        //Debug.Log("Enemy Moved");
    }
}