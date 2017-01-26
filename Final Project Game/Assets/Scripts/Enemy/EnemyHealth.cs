using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public Slider healthBar;
    private TurnHandler turnHandler;
    private Player player;

    public int health, looseHealthAmount, maxHealth;

    private float maxHealthBarScale;


    void Start() {
        turnHandler = FindObjectOfType<TurnHandler>();
        player = FindObjectOfType<Player>();
    }


    public void UpdateHealth(int healthAmount) {
        if(healthAmount < 0) {
            if(health > 0) {
                health -= looseHealthAmount;
            }

            if(health < 0) {
                health = 0;
            }
        } /*else { //THIS IS FOR IF YOU WANT ENEMIES TO GAIN HEALTH FROM SOMETHING
            if(health < maxHealth) {
                health += healthAmount;
            }

            if(health > maxHealth) {
                health = maxHealth;
            }
        }*/

        if(health == 0) {
            Debug.Log("Enemy Died");

            player.health.UpdateHealth(5);

            for(int i = 0; i < turnHandler.enemyList.Count; ++i) {
                if(gameObject.GetComponent<EnemyMovement>().currentTileNumber == turnHandler.enemyList[i].GetComponent<EnemyMovement>().currentTileNumber) {
                    turnHandler.enemyList.Remove(turnHandler.enemyList[i]);
                }
            }

            Destroy(gameObject);
            //healthBar.transform.localScale = new Vector3(0.0f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        } else {
            //Debug.Log(healthBar.transform.localScale + "- Health: " + health + ", " + " Max Health: " + maxHealth + ", " + " Max Health Bar Scale: " + maxHealthBarScale + ": " + (float)(((float)health / (float)maxHealth) * (float)maxHealthBarScale));
            healthBar.value = health;
        }
    }
}