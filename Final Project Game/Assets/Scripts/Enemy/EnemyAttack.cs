using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public Player player;

    //This is fired when it is detected that the player is in a tile that the enemy can attack in.
    public void EnemyAttackLogic() {
        player.health.UpdateHealth(-player.health.looseHealthAmount);
    }
}
