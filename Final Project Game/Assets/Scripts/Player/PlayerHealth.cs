using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Slider healthBar;

    [SerializeField]
    private TurnHandler turnHandler;

    public int health, looseHealthAmount, maxHealth;


    public void UpdateHealth(int healthAmount) {
        //If the given parameter is negative,
        if(healthAmount < 0) {
            //If the Player is alive, negate health.
            if(health > 0) {
                health -= looseHealthAmount;
            }

            //If negating health made the Player have less than 0 health, set it back to 0. (For a split second before the game is over, this is helpful for the visual aspect).
            if(health < 0) {
                health = 0;
            }

        } else { //add health if parameter given was not less than 0.
            if(health < maxHealth) {
                health += healthAmount;
            }

            if(health > maxHealth) {
                health = maxHealth;
            }
        }

        if(health == 0) {
            healthBar.value = 0;

            //Tried to fix the "InvalidOperationException: Collection was modified" error, failed, it has been added to the Backlog on Hack n Plan.
            //turnHandler.levelSet = false;

            //Destory every enemy on the board
            for(int i = 0; i < turnHandler.enemyList.Count; ++i) {
                Destroy(turnHandler.enemyList[i].gameObject);
            }

            //Clear the list of enemies.
            turnHandler.enemyList.Clear();

            //You lost, display the loser screen.
            turnHandler.lostGameUI.SetActive(true);
            turnHandler.gameRestarted = true;
            turnHandler.TilesBackToPurple();

        } else {//Player is not dead yet, update health bar.
            healthBar.value = health;
        }
    }
}
