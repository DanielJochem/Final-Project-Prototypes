using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    private EnemyAttack attack;
    private TurnHandler turnHandler;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        attack = FindObjectOfType<EnemyAttack>();
    }
    

    public void Attack() {
        attack.EnemyAttackLogic();
    }
}
