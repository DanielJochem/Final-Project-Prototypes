using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePlacer : MonoBehaviour {

    [Header("Manually Dragged-In")]
    public PlayerMovement playerMovement;
    public TurnHandler turnHandler;

    [SerializeField]
    private GameObject tileGridParent, enemiesParent;

    [SerializeField]
    private GameObject tilePrefab, enemyPrefab;


    [Header("Number Variables")]
    [SerializeField]
    private int xTiles, yTiles, tileSize, enemiesWanted;


    [HideInInspector]
    public List<GameObject> tiles, enemyList;

    private float xStartPos, yStartPos;

    private int randomTileToSpawnOnEnemyX, randomTileToSpawnOnEnemyY;


    void Start() {
        playerMovement.xTilesAmount = xTiles;

        PlaceTiles();
        SetUpPlayer();
        SetUpEnemies();

        turnHandler.levelSet = true;
    }


    void PlaceTiles() {
        if(xTiles % 2 == 0) {
            xStartPos = -((xTiles * tileSize) / 2) + ((tileSize / 2) + 0.5f);
        } else {
            xStartPos = -((xTiles * tileSize) / 2) + ((tileSize / 2));
        }

        if(yTiles % 2 == 0) {
            yStartPos = ((yTiles * tileSize) / 2) + ((tileSize / 2) - 0.5f);
        } else {
            yStartPos = ((yTiles * tileSize) / 2) + ((tileSize / 2));
        }

        Vector3 currPos = new Vector3(xStartPos, yStartPos, 2);

        for(int y = 0; y < yTiles; ++y) {
            for(int x = 0; x < xTiles; ++x) {
                GameObject tempObj;
                tempObj = Instantiate(tilePrefab, currPos, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;

                tiles.Add(tempObj);
                tempObj.GetComponent<TileListNumber>().listNum = tiles.Count;
                tempObj.transform.SetParent(tileGridParent.transform);

                //Next X position
                currPos.x += tileSize;
            }
            //End of a row? Next Y positionand start form first X position again.
            currPos = new Vector3(xStartPos, currPos.y -= tileSize, 2);
        }
    }


    void SetUpPlayer() {
        int randomTileToSpawnOnPlayerX = Random.Range(1, xTiles);
        int randomTileToSpawnOnPlayerY = Random.Range(1, yTiles);

        playerMovement.gameObject.transform.position = tiles[(randomTileToSpawnOnPlayerX * randomTileToSpawnOnPlayerY) - 1].transform.position;
        playerMovement.currentTileNumber = randomTileToSpawnOnPlayerX * randomTileToSpawnOnPlayerY;
    }


    void SetUpEnemies() {
        for(var i = 0; i < enemiesWanted; ++i) {
            bool checkForSpawnPosition = true;
            while(checkForSpawnPosition) {
                randomTileToSpawnOnEnemyX = Random.Range(1, xTiles);
                randomTileToSpawnOnEnemyY = Random.Range(1, yTiles);

                //We don't want to spawn enemies in any of the 4 corners, trust me, its gross, so this if statement checks if the current X and Y to spawn an enemy is in any of the 4 devilish corners.
                if(!((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == 1) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == xTiles) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == (xTiles * yTiles)) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == ((xTiles * yTiles) - (xTiles - 1)))) {
                    //Check to see if the current X and Y to spawn an enemy at would spawnn the enemy on top of the character.
                    if((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) != playerMovement.currentTileNumber) {
                        //Debug.Log("Checking for Enemy number " + (i + 1) + "...");
                        if(enemyList.Count > 0) {
                            checkForSpawnPosition = false;
                            foreach(GameObject enemyCheckPos in enemyList) {
                                if((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) != enemyCheckPos.GetComponent<EnemyMovement>().currentTileNumber) {
                                    continue;
                                } else {
                                    //Debug.Log("Enemy already at wanted position.");
                                    checkForSpawnPosition = true;
                                    break;
                                }
                            }
                            //Location is clear, place Enemy.
                        } else {
                            //Debug.Log("No Enemies in enemyList, place Enemy.");
                            checkForSpawnPosition = false;
                        }
                    }
                }
            }

            GameObject enemy = Instantiate(enemyPrefab, tiles[(randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) - 1].transform.position, Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyMovement>().currentTileNumber = randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY;
            enemy.GetComponent<EnemyMovement>().xTilesAmount = xTiles;
            enemy.GetComponent<EnemyMovement>().yTilesAmount = yTiles;

            enemyList.Add(enemy);
            enemy.transform.SetParent(enemiesParent.transform);
        }

        turnHandler.enemyList = enemyList;
    }
}
