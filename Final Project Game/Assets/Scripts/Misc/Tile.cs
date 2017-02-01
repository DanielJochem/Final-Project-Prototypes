using UnityEngine;

public class Tile : MonoBehaviour {
    private Player player;
    private TurnHandler turnHandler;

    private GameObject currentlySelectedEnemy;

    //What number am I in the tileList?
	public int listNum;


    void Start() {
        player = FindObjectOfType<Player>();
        turnHandler = FindObjectOfType<TurnHandler>();
    }


    //If an enemy was clicked on to see it's attackable tiles, show those tiles.
    public void OnMouseDown() {
        foreach(GameObject enemy in turnHandler.enemyList) {
            if(enemy.GetComponent<EnemyMovement>().currentTileNumber == listNum) {
                currentlySelectedEnemy = enemy;
                enemy.GetComponent<EnemyAttack>().EnemyDisplayAttackableTiles();
            }
        }
    }


    //When tile is clicked on,
	public void OnMouseUp() {
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
                }
            }
        }

        //If there was no enemy on the clicked tile,
        if(!turnHandler.playerAttackInsteadOfMove) {
            if(player.movement.direction == "") {
                //Debug.Log("I am List Number: " + listNum + ". My Position is: " + gameObject.transform.GetChild(0).GetChild(0).transform.position);
                //Set the clicked tile as the wantedTile.
                player.movement.wantedTileNumber = listNum;
            }
        }

        //De-Red-ify tiles if an enemy was clicked.
        if(currentlySelectedEnemy != null) {
            currentlySelectedEnemy.GetComponent<EnemyAttack>().EnemyRemoveAttackableTiles();
            currentlySelectedEnemy = null;
        }
	}
}
