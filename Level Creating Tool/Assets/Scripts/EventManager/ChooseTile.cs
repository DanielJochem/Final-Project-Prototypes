using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChooseTile : MonoBehaviour {
    public List<GameObject> UITiles;
    public Sprite placeSprite;
    public TilePlacer tilePlacer;

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
        if(!xTilesIF.isFocused && !yTilesIF.isFocused && !levelNameIF.isFocused) {
            switch(a_event.PressedKeyCode.ToString()) {
                case "Alpha1":
                    UITiles[0].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[0].transform.GetChild(0).gameObject;
                    break;
                case "Alpha2":
                    UITiles[1].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[1].transform.GetChild(0).gameObject;
                    break;
                case "Alpha3":
                    UITiles[2].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[2].transform.GetChild(0).gameObject;
                    break;
                case "Alpha4":
                    UITiles[3].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[3].transform.GetChild(0).gameObject;
                    break;
                case "Alpha5":
                    UITiles[4].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[4].transform.GetChild(0).gameObject;
                    break;
                case "Alpha6":
                    UITiles[5].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[5].transform.GetChild(0).gameObject;
                    break;
                case "Alpha7":
                    UITiles[6].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[6].transform.GetChild(0).gameObject;
                    break;
                case "Alpha8":
                    UITiles[7].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[7].transform.GetChild(0).gameObject;
                    break;
                case "Alpha9":
                    UITiles[8].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[8].transform.GetChild(0).gameObject;
                    break;
                case "Alpha0":
                    UITiles[9].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[9].transform.GetChild(0).gameObject;
                    break;
                case "Minus":
                    UITiles[10].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[10].transform.GetChild(0).gameObject;
                    break;
                case "Equals":
                    UITiles[11].GetComponent<Image>().sprite = placeSprite;
                    tilePlacer.selectedTile = UITiles[11].transform.GetChild(0).gameObject;
                    break;
            }
        }
    }
}
