using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public class UIRelatedStuff : MonoBehaviour {

	private TilePlacer tilePlacer;
	private LevelLoader levelLoader;
	private UIRelatedStuff uiRelatedStuff;
	private GameObject backdrop;

	[HideInInspector]
	public int tileSize;

	[HideInInspector]
	public List<GameObject> tiles;

	[SerializeField]
	private Text fileExistsText, saveErrorText, levelDoesNotExistErrorText, overwriteProgressErrorText, sameLevelErrorText;

	[HideInInspector]
	public bool levelLoaded, xySaved, thisIsACSVLoadedLevel, cameFromLoadingLevel;

	private bool canSave, cameFromIF, firstTimeLoadingALevel, goodNowBad;

	public InputField xTilesIF, yTilesIF, levelNameIF;
	public EventSystem eventSystem;

	private List<string> saveErrorArray;

	[HideInInspector]
	public int xTiles, yTiles;

	[HideInInspector]
	public string levelName;

	void Awake() {
		tilePlacer = FindObjectOfType<TilePlacer>();
		levelLoader = FindObjectOfType<LevelLoader>();
		uiRelatedStuff = FindObjectOfType<UIRelatedStuff>();
		backdrop = GameObject.Find("Backdrop");

		tileSize = 5;

		xTiles = yTiles = 0;
		eventSystem.firstSelectedGameObject = xTilesIF.gameObject;
	}

	void Update() {
		//Switching between the X and Y amount input fields at the start of the game.
		if(Input.GetKeyDown(KeyCode.Tab)) {
			//I think I am addicted to ternary operators...
			eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject == xTilesIF.gameObject ? yTilesIF.gameObject : xTilesIF.gameObject);
		}

		LoadTheLevel();
	}

	public void LoadTheLevel() {
		//Saving the X and Y Input fields, we can now load the level.
		if((Input.GetKeyDown(KeyCode.Return) && xTiles > 0 && yTiles > 0 && !xySaved) || thisIsACSVLoadedLevel) {
			xTilesIF.interactable = false;
			yTilesIF.interactable = false;

			tiles.Clear();
			xySaved = true;
			thisIsACSVLoadedLevel = false;
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
						tempObj = Instantiate(tilePlacer.placementTile, currPos, Quaternion.Euler(270.0f, 0.0f, 0.0f)) as GameObject;
						levelLoader.placementTilesGridList.Add(tempObj);

						tiles.Add(tempObj);
						tempObj.transform.GetChild(0).GetComponent<PlacementTileListNumber>().listNum = tiles.Count;

						//Next X position
						currPos.x += tileSize;
					}
					//End of a row? Next Y position and start form first X position again.
					currPos = new Vector3(xStartPos, currPos.y -= tileSize, 2);
				}

				levelLoader.isFromLevelLoader = false;

				//Backdrop re-scaling based on how many tiles x and y was entered.
				//The '+ 0.1f' is so there is a 0.1 boarder around the tiles.
				backdrop.transform.localScale = new Vector3((0.5f * xTiles) + 0.1f, 0, (0.5f * yTiles) + 0.1f);

				//If x or y or both were an odd number, fix up the position of the backdrop by 0.5.
				if(xTiles % 2 == 1 && yTiles % 2 == 0) {
					backdrop.transform.position = new Vector3(backdrop.transform.position.x + 0.5f, backdrop.transform.position.y, 2);
				} else if(xTiles % 2 == 0 && yTiles % 2 == 1) {
					backdrop.transform.position = new Vector3(backdrop.transform.position.x, backdrop.transform.position.y - 0.5f, 2);
				} else if(xTiles % 2 == 1 && yTiles % 2 == 1) {
					if(firstTimeLoadingALevel || goodNowBad) {
						backdrop.transform.position = new Vector3(backdrop.transform.position.x + 0.5f, backdrop.transform.position.y - 0.5f, 2);
						goodNowBad = false;
					}
				}

				if(!firstTimeLoadingALevel) {
					if(xTiles % 2 == 0 && yTiles % 2 == 0) {
						backdrop.transform.position = new Vector3(0, 0, 2);
						goodNowBad = true;
					}
				}

				firstTimeLoadingALevel = false;

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

	public void EnableSameLevelErrorText() {
		sameLevelErrorText.gameObject.SetActive(true);
	}

	public void DisableSameLevelErrorText() {
		sameLevelErrorText.gameObject.SetActive(false);
	}

	public void EnableLevelDoesNotExistErrorText() {
		levelDoesNotExistErrorText.gameObject.SetActive(true);
	}

	public void DisableLevelDoesNotExistErrorText() {
		levelDoesNotExistErrorText.gameObject.SetActive(false);
		levelLoader.alreadyCheckedFileExists = false;
	}

	public void EnableOverwriteProgressErrorText() {
		overwriteProgressErrorText.gameObject.SetActive(true);
	}

	public void DisableOverwriteProgressErrorText() {
		overwriteProgressErrorText.gameObject.SetActive(false);
	}

	public void OverwriteProgress() {
		DisableOverwriteProgressErrorText();
		DisableSameLevelErrorText();
		levelLoader.canOverwriteLevel = true;
		levelLoader.loadLevel = true;
		levelLoader.LoadLevel();
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

	public void GetRidOfSelectedTile() {
		//Just some safety for if the save button is just about to be pressed, as we don't want to place a sneaky, un-wanted tile before it saves to CSV.
		tilePlacer.selectedTile = null;
		tilePlacer.selectedTileSprite = null;
	}

	public void TestForErrorOnSave() {
		StringBuilder saveCheckSB = new StringBuilder();

		int chestsPlaced = 0, chestKeysPlaced = 0, doorsPlaced = 0, doorKeysPlaced = 0;

		for(int i = 0; i < FindObjectsOfType<SpriteRenderer>().Length; ++i) {
			if(FindObjectsOfType<SpriteRenderer>()[i].sprite.name == "Chest") {
				chestsPlaced++;
			}

			if(FindObjectsOfType<SpriteRenderer>()[i].sprite.name == "Chest Key") {
				chestKeysPlaced++;
			}

			if(FindObjectsOfType<SpriteRenderer>()[i].sprite.name == "Door") {
				doorsPlaced++;
			}

			if(FindObjectsOfType<SpriteRenderer>()[i].sprite.name == "Door Key") {
				doorKeysPlaced++;
			}
		}

		if(chestsPlaced != chestKeysPlaced) {
			saveCheckSB.Append("The amount of Chests and Chest Keys do not match.").AppendLine();
		}

		if(doorsPlaced != doorKeysPlaced) {
			saveCheckSB.Append("The amount of Doors and Door Keys do not match.").AppendLine();
		}

		//If there are places that haven't been filled with void
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
				saveCheckSB.Append("There is no input for Y Amount.").AppendLine();

			} else if(int.Parse(yTilesIF.text) <= 0) {
				saveCheckSB.Append("Y Amount is less than or equal to 0.").AppendLine();
			}

			if(tilePlacer.playerTilePlaced == null) {
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

		saveSB.Append(levelName).Append(",");
		saveSB.Append(xTiles).Append(",");
		saveSB.Append(yTiles).Append(",");
		saveSB.AppendLine();

		for(int i = 0; i < tiles.Count; ++i) {
			saveSB.Append(tiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name).Append(",");

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
				tiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = tilePlacer.voidTileSprite;
				tiles[i].transform.GetChild(0).GetComponent<PlacementTileListNumber>().isBlank = false;
			}
		}
	}
}