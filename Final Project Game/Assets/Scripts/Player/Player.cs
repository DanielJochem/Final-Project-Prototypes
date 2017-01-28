using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public PlayerMovement movement;
    public PlayerAttack attack;
    public PlayerHealth health;

    [SerializeField]
    private TurnHandler turnHandler;


    public void Movement() {
        movement.PlayerMovementLogic();
    }


    public void Attack(GameObject enemyToAttack) {
        attack.PlayerAttackLogic(enemyToAttack);
        //In case the Player chose to attack instead of move, set it back to the default of moving next turn.
        turnHandler.playerAttackInsteadOfMove = false;
        turnHandler.turnNumber++;
    }
}
