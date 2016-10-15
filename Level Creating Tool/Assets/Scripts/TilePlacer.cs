using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public class TilePlacer : MonoBehaviour {

    public GameObject selectedTile, placementTile;
    public Sprite selectedTileSprite, placementTileSprite, voidTileSprite;
    public GameObject backdrop; /**/
    private GameObject playerTilePlaced;
    public int tileSize;
    public List<GameObject> tiles;

    [SerializeField]
    private Text fileExistsText;

    [SerializeField]
    private Text saveErrorText;

    private bool levelLoaded, canSave;

    public InputField xTilesIF, yTilesIF, levelNameIF;
    public EventSystem eventSystem;
    public bool xySaved;

    public List<string> saveErrorArray;

    public int xTiles, yTiles;
    public string levelName;

    void Awake() {
        xTiles = yTiles = 0;
        eventSystem.firstSelectedGameObject = xTilesIF.gameObject;

        //Just in case.
        eventSystem.SetSelectedGameObject(xTilesIF.gameObject);
    }
    
	void Update () {

        //Switching between the X and Y amount input fields at the start of the game.
        if(Input.GetKeyDown(KeyCode.Tab)) {
            //I think I am addicted to ternary operators...
            eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject == xTilesIF.gameObject ? yTilesIF.gameObject : xTilesIF.gameObject);
        }

        //Saving the X and Y Input fields, we can now load the level.
        if(Input.GetKeyDown(KeyCode.Return) && xTiles > 0 && yTiles > 0 && !xySaved) {
            xTilesIF.interactable = false;
            yTilesIF.interactable = false;
            xySaved = true;
        }

        //Loading the level
        if(!levelLoaded) {
            if(xySaved) {
                float xStartPos = -((xTiles * tileSize) / 2) + ((tileSize / 2) + 0.5f);
                float yStartPos = ((yTiles * tileSize) / 2) - ((tileSize / 2) + 0.5f);
                Vector3 currPos = new Vector3(xStartPos, yStartPos, 2);

                for(int y = 0; y < yTiles; ++y) {
                    for(int x = 0; x < xTiles; ++x) {
                        GameObject tempObj;
                        tempObj = Instantiate(placementTile, currPos, Quaternion.Euler(270.0f, 0.0f, 0.0f));

                        tiles.Add(tempObj);
                        tempObj.transform.GetChild(0).GetComponent<PlacementTileListNumber>().listNum = tiles.Count;

                        //Next X position
                        currPos.x += tileSize;
                    }
                    //End of a row? Next Y positionand start form first X position again.
                    currPos = new Vector3(xStartPos, currPos.y -= tileSize, 2);
                }

                //Backdrop re-scaling based on how many tiles x and y was entered.
                //The '+ 0.1f' is so there is a 0.1 boarder around the tiles.
                backdrop.transform.localScale = new Vector3((0.5f * xTiles) + 0.1f, 0, (0.5f * yTiles) + 0.1f);

                //If x or y or both were an odd number, fix up the position of the backdrop by 0.5.
                if(xTiles % 2 == 1 && yTiles % 2 == 0) {
                    backdrop.transform.position = new Vector3(backdrop.transform.position.x + 0.5f, backdrop.transform.position.y, 2);
                } else if(xTiles % 2 == 0 && yTiles % 2 == 1) {
                    backdrop.transform.position = new Vector3(backdrop.transform.position.x, backdrop.transform.position.y - 0.5f, 2);
                } else if(xTiles % 2 == 1 && yTiles % 2 == 1) {
                    backdrop.transform.position = new Vector3(backdrop.transform.position.x + 0.5f, backdrop.transform.position.y - 0.5f, 2);
                }
                
                levelLoaded = true;
            }
        }
	}

    void EnableSaveErrorText() {
        saveErrorText.gameObject.SetActive(true);
    }

    public void DisableSaveErrorText() {
        saveErrorText.gameObject.SetActive(false);
    }

    void EnableFileExistsText() {
        fileExistsText.gameObject.SetActive(true);
    }

    public void OverrideFileExists() {
        DisableFileExistsText();
        SaveLevel();
    }

    public void DisableFileExistsText() {
        fileExistsText.gameObject.SetActive(false);
    }

    public void SetXTiles(InputField inputField) {
        xTiles = int.Parse(inputField.text);
    }

    public void SetYTiles(InputField inputField) {
        yTiles = int.Parse(inputField.text);
    }

    public void SetLevelName(InputField inputField) {
        levelName = inputField.text;
    }

    public void OnTileClicked(GameObject tile) {
        if(selectedTile != null) {
            if(selectedTile.name == "Player") {
                //If there is already a Player tile somewhere on the gid of tiles, get rid of it.
                if(playerTilePlaced != null) {
                    playerTilePlaced.GetComponent<SpriteRenderer>().sprite = placementTileSprite;
                    playerTilePlaced.GetComponent<PlacementTileListNumber>().isBlank = true;
                }

                //If you are clicking on the same tile as there is already a Player tile on, you obviously want to get rid of it.
                if(playerTilePlaced == tile) {
                    playerTilePlaced.GetComponent<SpriteRenderer>().sprite = placementTileSprite;
                    playerTilePlaced.GetComponent<PlacementTileListNumber>().isBlank = true;
                    playerTilePlaced = null;
                } else {
                    //Place the Player tile here
                    tile.GetComponent<SpriteRenderer>().sprite = selectedTileSprite;
                    tile.GetComponent<PlacementTileListNumber>().isBlank = false;
                    playerTilePlaced = tile;
                }

            } else {
                //Don't worry about the Player tile logic, just place the selected tile here.
                if(tile.GetComponent<SpriteRenderer>().sprite == selectedTileSprite) {
                    tile.GetComponent<SpriteRenderer>().sprite = placementTileSprite;
                    tile.GetComponent<PlacementTileListNumber>().isBlank = true;
                } else {
                    tile.GetComponent<SpriteRenderer>().sprite = selectedTileSprite;
                    tile.GetComponent<PlacementTileListNumber>().isBlank = false;
                }
            }
        }
    }

    public void GetRidOfSelectedTile() {
        //Just some safety for if the save button is just about to be pressed, as we don'w want to place a sneaky, un-wanted tile before it saves to CSV.
        selectedTile = null;
        selectedTileSprite = null;
    }

    public void TestForErrorOnSave() {
        StringBuilder saveCheckSB = new StringBuilder();

        //if there are places that havent been filled with void
        for(int i = 0; i < tiles.Count; ++i) {
            if(tiles[i].transform.GetChild(0).GetComponent<PlacementTileListNumber>().isBlank) {
                saveCheckSB.Append("Not all tiles have an object, try doing a Void Fill.").AppendLine();
                break;
            }
        }

        if(levelNameIF.text.Length == 0) {
            saveCheckSB.Append("There is no input for Level Name.").AppendLine();
        }

        if(xTilesIF.text.Length == 0 && yTilesIF.text.Length == 0) {
            saveCheckSB.Append("There are no inputs for both X Amount and Y Amount.").AppendLine();

        } else if(int.Parse(xTilesIF.text) <= 0 && int.Parse(yTilesIF.text) <= 0) {
            saveCheckSB.Append("Both X Amount and Y Amount are less than or equal to 0.").AppendLine();

        } else {
            if(xTilesIF.text.Length == 0) {
                saveCheckSB.Append("There is no input for X Amount.").AppendLine();

            } else if(int.Parse(xTilesIF.text) <= 0) {
                saveCheckSB.Append("X Amount is less than or equal to 0.").AppendLine();
            }

            if(yTilesIF.text.Length == 0) {
                saveCheckSB.Append("There is no input for Y AMount.").AppendLine();

            } else if(int.Parse(yTilesIF.text) <= 0) {
                saveCheckSB.Append("Y Amount is less than or equal to 0.").AppendLine();
            }

            if(playerTilePlaced == null) {
                saveCheckSB.Append("There is no Player tile placed on the grid.").AppendLine();
            }
        }

        if(saveCheckSB.Length > 0) {
            saveCheckSB.Remove(saveCheckSB.Length - 1, 1);
            saveErrorText.text = "Can not save because:\n" + saveCheckSB.ToString();
            EnableSaveErrorText();
            saveCheckSB.Remove(0, saveCheckSB.Length - 1);
        } else {
            if(System.IO.File.Exists(Application.dataPath + "/Saved Levels/" + levelName + ".csv")) {
                EnableFileExistsText();
            } else {
                //Can Save!
                SaveLevel();
            }
        }
    }

    void SaveLevel() {
        StringBuilder saveSB = new StringBuilder();

        saveSB.Append(levelName).Append(", ");
        saveSB.Append(xTiles).Append(", ");
        saveSB.Append(yTiles).Append(", ");
        saveSB.AppendLine();

        for(int i = 0; i < tiles.Count; ++i) {
            saveSB.Append(tiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name).Append(", ");

            if((i + 1) % xTiles == 0) {
                saveSB.AppendLine();
            }
        }

        System.IO.File.WriteAllText(Application.dataPath + "/Saved Levels/" + levelName + ".csv", saveSB.ToString());
        Debug.Log("Saved Successfully!");
    }

    public void VoidFillLevel() {
        for(int i = 0; i < tiles.Count; ++i) {
            if(tiles[i].transform.GetChild(0).GetComponent<PlacementTileListNumber>().isBlank) {
                tiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = voidTileSprite;
                tiles[i].transform.GetChild(0).GetComponent<PlacementTileListNumber>().isBlank = false;
            }
        }
    }
}