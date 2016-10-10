using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilePlacer : MonoBehaviour {

    public GameObject selectedTile, placementTile;
    public GameObject backdrop; /**/
    public int tileSize;

    private bool levelLoaded;

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
        //For placing tiles
        if(selectedTile != null) {

        }

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

                        //Gross scaling stuff
                        Vector3 scaleLocal = tempObj.transform.localScale;
                        scaleLocal.z *= 2;
                        scaleLocal /= 2;
                        tempObj.transform.localScale = scaleLocal;
                        //Eww, glad that is over!
                        
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

    public void SetXTiles(InputField inputField) {
        xTiles = int.Parse(inputField.text);
    }

    public void SetYTiles(InputField inputField) {
        yTiles = int.Parse(inputField.text);
    }

    public void SetLevelName(InputField inputField) {
        levelName = inputField.text;
    }

    public void TestForErrorOnSave() {
        //if there are places that havent been filled with void

        if(levelNameIF.text.Length == 0) {
            saveErrorArray.Add("there is no input for Level Name");
        }

        if(xTilesIF.text.Length == 0 && yTilesIF.text.Length == 0) {
            saveErrorArray.Add(saveErrorArray.Count > 0 ? "and there are no inputs for both X Amount and Y Amount" : "there are no inputs for both X Amount and Y Amount");

        } else if(int.Parse(xTilesIF.text) <= 0 && int.Parse(yTilesIF.text) <= 0) {
            saveErrorArray.Add("both X Amount and Y Amount are less than or equal to 0");

        } else {
            if(xTilesIF.text.Length == 0) {
                saveErrorArray.Add(saveErrorArray.Count > 0 ? "or" : "there is no input for");
                saveErrorArray.Add("X Amount");

            } else if(int.Parse(xTilesIF.text) <= 0) {
                if(saveErrorArray.Count > 0) {
                    saveErrorArray.Add("and");
                }

                saveErrorArray.Add("X Amount is less than or equal to 0");
            }

            if(yTilesIF.text.Length == 0) {
                //Yep... definitely addicted to ternary operators.
                saveErrorArray.Add(int.Parse(xTilesIF.text) <= 0 ? "and there is no input for" : saveErrorArray.Count > 0 ? "or" : "there is no input for");
                saveErrorArray.Add("Y Amount");

            } else if(int.Parse(yTilesIF.text) <= 0) {
                if(saveErrorArray.Count > 0) {
                    saveErrorArray.Add("and");
                }

                saveErrorArray.Add("Y Amount is less than or equal to 0");
            }
        }

        if(saveErrorArray.Count > 0) {
            string joined = string.Join(" ", saveErrorArray.ToArray());
            Debug.Log("Can not save because " + joined + ".");
            saveErrorArray.Clear();
        } else {
            Debug.Log("Saved Successfully!");
        }
    }
}
