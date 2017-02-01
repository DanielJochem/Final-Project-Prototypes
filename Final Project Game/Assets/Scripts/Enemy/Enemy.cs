using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    [HideInInspector]
    public EnemyAttack attack;
    [HideInInspector]
    public EnemyMovement movement;
    [HideInInspector]
    public TurnHandler turnHandler;

    [HideInInspector]
    public List<GameObject> gridOfTiles;

    [HideInInspector]
    public int xTilesAmount, yTilesAmount;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        attack = FindObjectOfType<EnemyAttack>();
        movement = FindObjectOfType<EnemyMovement>();
    }


    public void Attack() {
        attack.EnemyAttackLogic();
    }
}
