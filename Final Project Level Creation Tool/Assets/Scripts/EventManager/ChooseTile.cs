using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChooseTile : MonoBehaviour {
    public TilePlacer tilePlacer;
    public TileSwapper tileSwapper;
    public UIRelatedStuff uiRelatedStuff;

    public List<GameObject> UITiles;
    public Sprite placeSprite, swapSprite;

    public InputField xTilesIF;
    public InputField yTilesIF;
    public InputField levelNameIF;

    void Awake () {
        EventManager.AddListener<KeyPressedEvent>(OnKeyPressed);
	}
    
    void OnDestroy() {
        EventManager.RemoveListener<KeyPressedEvent>(OnKeyPressed);
    }

    void OnKeyPressed(KeyPressedEvent a_event) {
        if(uiRelatedStuff.xySaved && !uiRelatedStuff.levelNameIF.isFocused) {
            switch(a_event.PressedKeyCode.ToString()) {
                case "Alpha1":
                case "Keypad1":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[0].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[0].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[0].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[0].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[0].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha2":
                case "Keypad2":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[1].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[1].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[1].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[1].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha3":
                case "Keypad3":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[2].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[2].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[2].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[2].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[2].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha4":
                case "Keypad4":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[3].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[3].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[3].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[3].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[3].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha5":
                case "Keypad5":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[4].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[4].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[4].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[4].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[4].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha6":
                case "Keypad6":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[5].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[5].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[5].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[5].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[5].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha7":
                case "Keypad7":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[6].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[6].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[6].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[6].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[6].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha8":
                case "Keypad8":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[7].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[7].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[7].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[7].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[7].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha9":
                case "Keypad9":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[8].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[8].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[8].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[8].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[8].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Alpha0":
                case "KeypadDivide":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[9].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[9].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[9].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[9].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[9].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Minus":
                case "KeypadMultiply":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[10].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[10].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[10].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[10].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[10].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
                case "Equals":
                case "KeypadMinus":
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        UITiles[11].GetComponent<Image>().sprite = swapSprite;
                        tileSwapper.ToSwap(UITiles[11].transform.GetChild(0).gameObject);
                    } else {
                        UITiles[11].GetComponent<Image>().sprite = placeSprite;
                        tilePlacer.selectedTile = UITiles[11].transform.GetChild(0).gameObject;
                        tilePlacer.selectedTileSprite = UITiles[11].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    }
                    break;
            }
        }
    }
}