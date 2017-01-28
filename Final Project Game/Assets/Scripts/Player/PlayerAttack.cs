using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public void PlayerAttackLogic(GameObject enemyToAttack) {
        //Negate the enemy's health
        enemyToAttack.GetComponent<EnemyHealth>().UpdateHealth(-enemyToAttack.GetComponent<EnemyHealth>().looseHealthAmount);
    }
}
