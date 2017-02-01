using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {
    private Player player;

    //This is a reference to the Enemy script that all enemies reference from.
    private Enemy enemy;

    [HideInInspector]
    public int currentTileNumber, xTilesAmount, yTilesAmount;

    public List<EnemyAttackPositions> enemyAttackablePositions;
    public List<GameObject> enemyAttackableTiles;

    private Color purple = new Color(0.5568f, 0.0156f, 0.8902f);
    private Color red = Color.red;

    private bool shownTiles;


    //Has to be Awake() otherwise xTilesAmount isn't set before CalculateAttackableTiles() is run.
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = FindObjectOfType<Enemy>();

        xTilesAmount = enemy.xTilesAmount;
        yTilesAmount = enemy.yTilesAmount;
    }


    //Just for debug showing which enemy you clicked on.
    public void ClickedEnemy() {
        Debug.Log(gameObject.name);
    }


    //This is fired when it is detected that the player is in a tile that the enemy can attack in.
    public void EnemyAttackLogic() {
        player.health.UpdateHealth(-player.health.looseHealthAmount);
    }


    public void EnemyDisplayAttackableTiles() {
        //For debug, show what enemy was clicked on.
        ClickedEnemy();

        foreach(GameObject attackableTile in enemyAttackableTiles) {
            attackableTile.GetComponent<SpriteRenderer>().color = red;
        }

        //shownTiles = true;
    }


    public void EnemyRemoveAttackableTiles() {
        foreach(GameObject attackableTile in enemyAttackableTiles) {
            attackableTile.GetComponent<SpriteRenderer>().color = purple;
        }

        //shownTiles = false;
    }


    public void CalculateAttackableTiles() {
        currentTileNumber = GetComponent<EnemyMovement>().currentTileNumber;

        foreach(EnemyAttackPositions position in enemyAttackablePositions) {
            if(position.positions != Vector2.zero) {
                GameObject tempTile;

                //For some unknown reason, it starts calculating enemy attackable tiles one tile to the right of the enemy, so I had to go "currentTileNumber - 1".
                if(((currentTileNumber - 1) + ((int)position.positions.x + ((int)position.positions.y * xTilesAmount)) < 1) //If the calculated tile number is less than 1 (handles Up tiles).
                    || ((currentTileNumber - 1) + ((int)position.positions.x + ((int)position.positions.y * xTilesAmount)) > xTilesAmount * yTilesAmount) //If the calculated tile number is greater than the last tile (handles Down tiles).
                    || Mathf.CeilToInt(((currentTileNumber - 1) + (int)position.positions.x) / xTilesAmount) != Mathf.CeilToInt((currentTileNumber - 1) / xTilesAmount)) //If the calculated tile number is not on the same row as the enemy (handles Left and Right tiles).
                {
                    continue;
                } else {
                    tempTile = enemy.gridOfTiles[(currentTileNumber - 1) + ((int)position.positions.x + ((int)position.positions.y * xTilesAmount))].gameObject;
                    enemyAttackableTiles.Add(tempTile);
                }

                //Show which tiles the enemy can attack on in Console.
                //Debug.Log((currentTileNumber - 1) + ((int)position.positions.x + ((int)position.positions.y * xTilesAmount)));
            }
        }
    }
}


[System.Serializable]
public class EnemyAttackPositions {
    public Vector2 positions;
}


[CustomPropertyDrawer(typeof(EnemyAttackPositions))]
public class ChangeInspector : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, GUIContent.none, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("positions"), GUIContent.none);
        contentPosition.x += contentPosition.width;
        contentPosition.width /= 10f;
        EditorGUIUtility.labelWidth = 14f;
        EditorGUI.EndProperty();
    }
}