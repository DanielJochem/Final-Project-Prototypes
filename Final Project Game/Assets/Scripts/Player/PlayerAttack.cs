using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    public TurnHandler turnHandler;

    public void PlayerAttackLogic(GameObject enemyToAttack) {
        //Negate the enemy's health
        enemyToAttack.GetComponent<EnemyHealth>().UpdateHealth(-enemyToAttack.GetComponent<EnemyHealth>().looseHealthAmount);

        //If the attack killed the last enemy,
        if(turnHandler.enemyList.Count == 0) {
            //You won this room! Congrats!
            turnHandler.wonGameUI.SetActive(true);
            turnHandler.gameRestarted = true;
        }
        
        gameObject.GetComponent<PlayerMovement>().wantedTileNumber = gameObject.GetComponent<PlayerMovement>().currentTileNumber;
        turnHandler.turnNumber++;
    }
}
