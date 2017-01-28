using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public Slider healthBar;
    private TurnHandler turnHandler;
    private Player player;

    public int health, looseHealthAmount, maxHealth;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        player = FindObjectOfType<Player>();
    }


    public void UpdateHealth(int healthAmount) {
        //If the given parameter is negative,
        if(healthAmount < 0) {
            //If enemy is alive, negate health.
            if(health > 0) {
                health -= looseHealthAmount;
            }

            //If negating health made the enemy have less than 0 health, set it back to 0. (For a split second before the enemy dies, this is helpful for the visual aspect).
            if(health < 0) {
                health = 0;
            }
        } /*else { //THIS IS FOR IF YOU WANT ENEMIES TO GAIN HEALTH FROM SOMETHING (else add health if parameter given was not less than 0).
            if(health < maxHealth) {
                health += healthAmount;
            }

            if(health > maxHealth) {
                health = maxHealth;
            }
        }*/

        if(health == 0) {
            Debug.Log("Enemy Died");

            //Add health to the player.
            player.health.UpdateHealth(5);

            //Find the now-dead enemy from the list of enemies and remove it.
            for(int i = 0; i < turnHandler.enemyList.Count; ++i) {
                if(gameObject.GetComponent<EnemyMovement>().currentTileNumber == turnHandler.enemyList[i].GetComponent<EnemyMovement>().currentTileNumber) {
                    turnHandler.enemyList.Remove(turnHandler.enemyList[i]);
                }
            }

            //Destory the enemy.
            Destroy(gameObject);

        } else { //Enemy is not dead yet, update health bar.
            healthBar.value = health;
        }
    }
}