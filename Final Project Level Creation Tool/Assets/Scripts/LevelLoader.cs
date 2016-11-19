using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	private UIRelatedStuff uiRelatedStuff;
	private CameraBehaviour cameraBehaviour;
	private TilePlacer tilePlacer;

	private int xTilesCSV, yTilesCSV, objectsPerRow, amountOfRows;

	private string[] loadedCSVString;

	[SerializeField]
	private GameObject wallObj, floorObj, doorObj, doorKeyObj, chestObj, chestKeyObj, playerObj, enemyObj, bossObj, armourObj, weaponObj, potionObj;

	[SerializeField]
	private GameObject placementTile;

	[HideInInspector]
	public List<GameObject> placementTilesGridList;

	private Vector3 currPos;

	[HideInInspector]
	public bool firstTime, isFromLevelLoader, alreadyCheckedFileExists;

	private bool fileExists;

	[HideInInspector]
	public bool loadLevel, canOverwriteLevel;

	[HideInInspector]
	public string currentLoadedLevel = "iAmSomeTempText";

	void Start() {
		uiRelatedStuff = FindObjectOfType<UIRelatedStuff>();
		cameraBehaviour = FindObjectOfType<CameraBehaviour>();
		tilePlacer = FindObjectOfType<TilePlacer>();
	}

	public void LoadLevel() {
		if(!alreadyCheckedFileExists) {
			fileExists = (System.IO.File.Exists(Application.dataPath + "/Saved Levels/" + uiRelatedStuff.levelName + ".csv") == true) ? true : false;

			alreadyCheckedFileExists = true;

			if(currentLoadedLevel == uiRelatedStuff.levelName) {
				uiRelatedStuff.EnableSameLevelErrorText();
				fileExists = false;
			} else if(fileExists && uiRelatedStuff.xySaved) {
				uiRelatedStuff.EnableOverwriteProgressErrorText();
				fileExists = false;
			} else if(fileExists) {
				loadLevel = true;
				currentLoadedLevel = uiRelatedStuff.levelName;
				fileExists = false;
			} else {
				uiRelatedStuff.EnableLevelDoesNotExistErrorText();
			}
		}

		if(canOverwriteLevel) {
			currentLoadedLevel = uiRelatedStuff.levelName;
			canOverwriteLevel = false;
		}

		if(loadLevel) {
			uiRelatedStuff.levelLoaded = false;

			if(uiRelatedStuff.xySaved) {
				uiRelatedStuff.xySaved = false;
				uiRelatedStuff.xTilesIF.interactable = false;
				uiRelatedStuff.yTilesIF.interactable = false;

				foreach(GameObject tile in placementTilesGridList) {
					Destroy(tile.gameObject);
				}

				placementTilesGridList.Clear();
			}

			uiRelatedStuff.levelLoaded = false;
			loadedCSVString = ReadCSV(uiRelatedStuff.levelName);

			firstTime = true;
			CSVLevelLoader();
			firstTime = false;

			uiRelatedStuff.xTiles = xTilesCSV;
			uiRelatedStuff.xTilesIF.text = uiRelatedStuff.xTiles.ToString();

			uiRelatedStuff.yTiles = yTilesCSV;
			uiRelatedStuff.yTilesIF.text = uiRelatedStuff.yTiles.ToString();

			cameraBehaviour.cameraHeightMax = 0;
			cameraBehaviour.cameraZooming();

			uiRelatedStuff.thisIsACSVLoadedLevel = true;

			isFromLevelLoader = true;
			uiRelatedStuff.LoadTheLevel();
			CSVLevelLoader();

			loadLevel = false;
			alreadyCheckedFileExists = false;
		}
	}

	public string[] ReadCSV(string levelNameCSV) {
		string getCSV = System.IO.File.ReadAllText(Application.dataPath + "/Saved Levels/" + uiRelatedStuff.levelName + ".csv");
		return getCSV.Split(new char[] { '\n', '\r', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
	}

	void CSVLevelLoader() {
		int currentTile = 0;

		if(firstTime) {
			for(int i = 0; i < 3; ++i) {
				switch(i) {
					case 0:
						continue;
					case 1:
						xTilesCSV = int.Parse(loadedCSVString[i]);
						break;
					case 2:
						yTilesCSV = int.Parse(loadedCSVString[i]);
						break;
				}
			}
		} else {
			for(int j = 0; j < loadedCSVString.Length; ++j) {
				switch(j) {
					case 0:
						continue;
					case 1:
						continue;
					case 2:
						continue;
					default:
						placementTilesGridList[currentTile].gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
								(loadedCSVString[j] == "Wall" ? wallObj
								: loadedCSVString[j] == "Floor" ? floorObj
								: loadedCSVString[j] == "Door" ? doorObj
								: loadedCSVString[j] == "Door Key" ? doorKeyObj
								: loadedCSVString[j] == "Chest" ? chestObj
								: loadedCSVString[j] == "Chest Key" ? chestKeyObj
								: loadedCSVString[j] == "Player" ? playerObj
								: loadedCSVString[j] == "Enemy" ? enemyObj
								: loadedCSVString[j] == "Boss" ? bossObj
								: loadedCSVString[j] == "Armour" ? armourObj
								: loadedCSVString[j] == "Weapon" ? weaponObj
								: potionObj).GetComponent<SpriteRenderer>().sprite;

						if(loadedCSVString[j] == "Player") {
							tilePlacer.playerTilePlaced = placementTilesGridList[currentTile].gameObject.transform.GetChild(0).gameObject;
						}

						currentTile++;
						break;
				}
			}

			foreach(GameObject tile in placementTilesGridList) {
				tile.transform.GetChild(0).gameObject.GetComponent<PlacementTileListNumber>().isBlank = false;
			}
		}
	}
}