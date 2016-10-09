using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITile : MonoBehaviour {
    [SerializeField]
    private Sprite baseSprite;

    [SerializeField]
    private Sprite swapSprite;

    [SerializeField]
    private Sprite placeSprite;

    //Swap Variables
    private TileSwapper tileSwapper;

    //Place Variables
    private TilePlacer tilePlacer;
    private GameObject selectedTile;

    void Awake() {
        tileSwapper = FindObjectOfType<TileSwapper>();
        tilePlacer = FindObjectOfType<TilePlacer>();
    }

    void Update() {
        if(tilePlacer.selectedTile == gameObject) {
            if(gameObject.transform.parent.gameObject.GetComponent<Image>().sprite != placeSprite) {
                gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = placeSprite;
            }
        } else {
            if(gameObject.transform.parent.gameObject.GetComponent<Image>().sprite != swapSprite) {
                BackToBaseSprite();
            }
        }
    }

    public void OnTileClick() {
        //Swapping
        if(Input.GetKey(KeyCode.LeftControl)) { 
            gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = swapSprite;
            tileSwapper.ToSwap(gameObject);

        //Placing
        } else { 
            gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = placeSprite;
            tilePlacer.selectedTile = gameObject;
        }
    }

    public void BackToBaseSprite() {
        gameObject.transform.parent.gameObject.GetComponent<Image>().sprite = baseSprite;
    }
}
