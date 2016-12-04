using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public void PlayerAttackLogic(GameObject enemyToAttack) {
        enemyToAttack.GetComponent<EnemyHealth>().UpdateHealth(-enemyToAttack.GetComponent<EnemyHealth>().looseHealthAmount);
    }
}
