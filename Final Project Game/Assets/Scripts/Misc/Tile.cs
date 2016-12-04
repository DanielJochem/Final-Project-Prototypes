using UnityEngine;

public class Tile : MonoBehaviour {
    private Player player;
    private TurnHandler turnHandler;

	public int listNum;


    void Start() {
        player = FindObjectOfType<Player>();
        turnHandler = FindObjectOfType<TurnHandler>();
    }


	public void OnMouseUp() {
        //Make sure we can't click ANY enemy on the board to attack (this long IF statement will only allow for closest tile in all 8 directions).
        if((player.movement.currentTileNumber - player.movement.xTilesAmount) == listNum                //Up
            || (player.movement.currentTileNumber - (player.movement.xTilesAmount - 1)) == listNum      //UpRight
            || (player.movement.currentTileNumber + 1) == listNum                                       //Right
            || (player.movement.currentTileNumber + (player.movement.xTilesAmount + 1)) == listNum      //DownRight
            || (player.movement.currentTileNumber + player.movement.xTilesAmount) == listNum            //Down
            || (player.movement.currentTileNumber + (player.movement.xTilesAmount - 1)) == listNum      //DownLeft
            || (player.movement.currentTileNumber - 1) == listNum                                       //Left
            || (player.movement.currentTileNumber - (player.movement.xTilesAmount + 1)) == listNum) {   //UpLeft

            foreach(GameObject enemy in turnHandler.enemyList) {
                if(enemy.GetComponent<EnemyMovement>().currentTileNumber == listNum) {
                    turnHandler.enemyToAttack = enemy;
                    turnHandler.playerAttackInsteadOfMove = true;
                }
            }
        }

        if(!turnHandler.playerAttackInsteadOfMove) {
            if(player.movement.direction == "") {
                //Debug.Log("I am List Number: " + listNum + ". My Position is: " + gameObject.transform.GetChild(0).GetChild(0).transform.position);
                player.movement.wantedTileNumber = listNum;
            }
        }
	}
}
