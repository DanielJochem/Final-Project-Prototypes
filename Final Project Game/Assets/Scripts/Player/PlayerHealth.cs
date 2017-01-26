using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    //[HideInInspector]
    public Slider healthBar;

    [SerializeField]
    private TurnHandler turnHandler;

    public int health, looseHealthAmount, maxHealth;


    public void UpdateHealth(int healthAmount) {
        if(healthAmount < 0) {
            if(health > 0) {
                health -= looseHealthAmount;
            }

            if(health < 0) {
                health = 0;
            }
        } else {
            if(health < maxHealth) {
                health += healthAmount;
            }

            if(health > maxHealth) {
                health = maxHealth;
            }
        }

        if(health == 0) {
            healthBar.value = 0;

            for(int i = 0; i < turnHandler.enemyList.Count; ++i) {
                Destroy(turnHandler.enemyList[i].gameObject);
            }

            turnHandler.enemyList.Clear();

            turnHandler.lostGameUI.SetActive(true);
        } else {
            healthBar.value = health;
            //healthBar.transform.localScale = new Vector3((float)(((float)health / (float)maxHealth) * (float)maxHealthBarScale), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
