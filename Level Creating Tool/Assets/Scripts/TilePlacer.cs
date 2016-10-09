using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilePlacer : MonoBehaviour {

    public GameObject selectedTile;
    private bool levelLoaded;

    public InputField xTilesIF;
    public InputField yTilesIF;
    public InputField levelNameIF;

    public List<string> saveErrorArray;

    public int xTiles, yTiles;
    public string levelName;

    void Awake() {
        xTiles = yTiles = 0;
    }
    
	void Update () {
        if(selectedTile != null) {

        }

        if(!levelLoaded) {
            if(xTiles > 0 && yTiles > 0) {

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
                if(saveErrorArray.Count > 0)
                    saveErrorArray.Add("and");

                saveErrorArray.Add("X Amount is less than or equal to 0");
            }

            if(yTilesIF.text.Length == 0) {
                saveErrorArray.Add(int.Parse(xTilesIF.text) <= 0 ? "and there is no input for" : saveErrorArray.Count > 0 ? "or" : "there is no input for");
                saveErrorArray.Add("Y Amount");

            } else if(int.Parse(yTilesIF.text) <= 0) {
                if(saveErrorArray.Count > 0)
                    saveErrorArray.Add("and");

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
