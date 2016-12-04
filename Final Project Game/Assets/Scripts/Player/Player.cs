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
        turnHandler.playerAttackInsteadOfMove = false;
        turnHandler.turnNumber++;
    }
}
