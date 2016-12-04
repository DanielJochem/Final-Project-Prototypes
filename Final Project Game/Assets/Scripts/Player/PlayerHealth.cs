using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    [HideInInspector]
    public GameObject healthBar;

    [SerializeField]
    private TurnHandler turnHandler;

    public int health, looseHealthAmount, maxHealth;

    [HideInInspector]
    public float maxHealthBarScale;


	void Start () {
        healthBar = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        maxHealthBarScale = healthBar.transform.localScale.x;
	}


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
            healthBar.transform.localScale = new Vector3(0.0f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

            for(int i = 0; i < turnHandler.enemyList.Count; ++i) {
                Destroy(turnHandler.enemyList[i].gameObject);
            }

            turnHandler.enemyList.Clear();

            turnHandler.lostGameUI.SetActive(true);
        } else {
            healthBar.transform.localScale = new Vector3((float)(((float)health / (float)maxHealth) * (float)maxHealthBarScale), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
