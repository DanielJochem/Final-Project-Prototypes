using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public Player player;

    public void EnemyAttackLogic() {
        player.health.UpdateHealth(-player.health.looseHealthAmount);
    }
}
