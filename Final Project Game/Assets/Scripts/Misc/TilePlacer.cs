using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is kind of a GameManager script, but only for the setup for each level.
public class TilePlacer : MonoBehaviour {
    private Player player;
    private TurnHandler turnHandler;

    private GameObject tileGridParent, enemiesParent;

    [Header("Manually Dragged-In")]
    [SerializeField]
    private GameObject tilePrefab;

    //[Space(5)]
    [SerializeField]
    private int xTiles, yTiles, tileSize;


    [Space(5)]
    [Header("Enemies To Spawn")]
    [SerializeField]
    private List<EnemySpawner> enemiesWanted;

    [HideInInspector]
    public List<GameObject> tiles, enemyList;

    private float xStartPos, yStartPos;

    private int randomTileToSpawnOnEnemyX, randomTileToSpawnOnEnemyY;


    void Start() {
        player = FindObjectOfType<Player>();
        turnHandler = FindObjectOfType<TurnHandler>();
        tileGridParent = GameObject.FindGameObjectWithTag("TileGrid");
        enemiesParent = GameObject.FindGameObjectWithTag("EnemiesParent");
        
        PlaceTiles();
        SetUpPlayer();
        SetUpEnemies();

        //Give the xTilesAmount to the PlayerMovement script.
        player.movement.xTilesAmount = xTiles;

        //Everything is now set up, the game may now begin.
        turnHandler.levelSet = true;
    }


    void PlaceTiles() {
        //Centre the gid of tiles on the screen.
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

        //Place all the tiles.
        for(int y = 0; y < yTiles; ++y) {
            for(int x = 0; x < xTiles; ++x) {
                GameObject tempObj;
                tempObj = Instantiate(tilePrefab, currPos, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;

                //Add the tile to the tiles list.
                tiles.Add(tempObj);

                //Set it's own special list number.
                tempObj.GetComponent<Tile>().listNum = tiles.Count;

                //Give it a parent to go to, to cry at night.
                tempObj.transform.SetParent(tileGridParent.transform);

                //Set the next X position for the next tile.
                currPos.x += tileSize;
            }

            //End of a row? Next Y position and start form first X position again.
            currPos = new Vector3(xStartPos, currPos.y -= tileSize, 2);
        }
    }


    void SetUpPlayer() {
        //Choose a random tile to start on.
        int randomTileToSpawnOnPlayerX = Random.Range(1, xTiles);
        int randomTileToSpawnOnPlayerY = Random.Range(1, yTiles);

        //Actually place the Player on that tile's position.
        player.gameObject.transform.position = tiles[(randomTileToSpawnOnPlayerX * randomTileToSpawnOnPlayerY) - 1].transform.position;
        player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, 1.5f);

        //Give the Player the currentTileNumber they are on, and give them no current wantedTile.
        player.movement.currentTileNumber = randomTileToSpawnOnPlayerX * randomTileToSpawnOnPlayerY;
        player.movement.wantedTileNumber = -1;
    }


    void SetUpEnemies() {
        //For every type of enemy wanted,
        for(int i = 0; i < enemiesWanted.Count; ++i) {
            //For all of that type of enemy,
            for(int j = 0; j < enemiesWanted[i].enemyAmount; j++) {
                //Check conditions for if the tile is available or not. Keep checking until the chosen tile is available.
                bool checkForSpawnPosition = true;
                while(checkForSpawnPosition) {
                    //Pick a random tile to be placed on.
                    randomTileToSpawnOnEnemyX = Random.Range(1, xTiles);
                    randomTileToSpawnOnEnemyY = Random.Range(1, yTiles);

                    //We don't want to spawn enemies in any of the 4 corners, trust me, its gross, so this if statement checks if the current X and Y to spawn an enemy is in any of the 4 devilish corners.
                    if(!((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == 1) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == xTiles) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == (xTiles * yTiles)) && !((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) == ((xTiles * yTiles) - (xTiles - 1)))) {
                        //Check to see if the current X and Y to spawn an enemy at would spawnn the enemy on top of the character.
                        if((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) != player.movement.currentTileNumber) {
                            //Debug.Log("Checking for Enemy number " + (i + 1) + "...");
                            //Now start checking if any other enemy is on that tile.
                            if(enemyList.Count > 0) {
                                checkForSpawnPosition = false;
                                foreach(GameObject enemyCheckPos in enemyList) {
                                    //If the current enemy checked from the enemyList ISN'T on the tile, check the next enemy in the enemyList.
                                    if((randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) != enemyCheckPos.GetComponent<EnemyMovement>().currentTileNumber) {
                                        continue;
                                    } else { //An enemy from the enemyList IS on the tile.
                                        //Debug.Log("Enemy already at wanted position.");
                                        checkForSpawnPosition = true;
                                        break;
                                    }
                                }
                                
                            } else {//Tile is clear, can place Enemy.
                                //Debug.Log("No Enemies in enemyList, place Enemy.");
                                checkForSpawnPosition = false;
                            }
                        }
                    }
                }

                //Spawn the enemy type,
                GameObject enemy = Instantiate(enemiesWanted[i].enemyType, tiles[(randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY) - 1].transform.position, Quaternion.Euler(135f, 90f, -90f)) as GameObject;
                
                //Set the position of the enemy onto the tile,
                enemy.gameObject.transform.position = new Vector3(enemy.gameObject.transform.position.x, enemy.gameObject.transform.position.y, 1.5f);

                //Give the eney ot's currentTileNumber,
                enemy.GetComponent<EnemyMovement>().currentTileNumber = randomTileToSpawnOnEnemyX * randomTileToSpawnOnEnemyY;

                //And pass the horizontal and vertical tile integers to the enemy, so it can calculate what it is allowed to move to.
                enemy.GetComponent<EnemyMovement>().xTilesAmount = xTiles;
                enemy.GetComponent<EnemyMovement>().yTilesAmount = yTiles;

                //Add the enemy to the enemyList.
                enemyList.Add(enemy);

                //Give the enemy a parent to go and cry to at night.
                enemy.transform.SetParent(enemiesParent.transform);
            }
        }

        //All enemies have been spawned and set, give  the list of enemies to the turnHandler.
        turnHandler.enemyList = enemyList;
    }

    public void RestartGame() {
        SetUpPlayer();
        SetUpEnemies();
    }
}


//Pick what type of enemies and how many of each enemy type you want to spawn into the current level.
[System.Serializable]
public class EnemySpawner {
    public GameObject enemyType;
    public int enemyAmount;
}
