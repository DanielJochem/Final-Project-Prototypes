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

    #region Reference Holders for other Enemy scripts.
    [HideInInspector]
    public int xTilesAmount, yTilesAmount;

    [HideInInspector]
    public GameObject currentlySelectedEnemy;
    public bool enemySwapped, currentSelectedEnemyIsDead;
    #endregion


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        attack = FindObjectOfType<EnemyAttack>();
        movement = FindObjectOfType<EnemyMovement>();
        currentlySelectedEnemy = null;
    }


    public void Attack() {
        attack.EnemyAttackLogic();
    }
}
