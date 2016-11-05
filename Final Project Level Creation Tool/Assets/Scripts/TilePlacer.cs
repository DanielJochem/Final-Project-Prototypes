using System.Collections;
using UnityEngine;

public class TilePlacer : MonoBehaviour {

    public GameObject selectedTile, placementTile;
    public Sprite selectedTileSprite, placementTileSprite, voidTileSprite;

    [HideInInspector]
    public GameObject playerTilePlaced;

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
                if(Input.GetMouseButton(1)) {
                    if(tile.GetComponent<SpriteRenderer>().sprite != placementTileSprite) {
                        tile.GetComponent<SpriteRenderer>().sprite = placementTileSprite;
                        tile.GetComponent<PlacementTileListNumber>().isBlank = true;
                    }
                } else {
                    tile.GetComponent<SpriteRenderer>().sprite = selectedTileSprite;
                    tile.GetComponent<PlacementTileListNumber>().isBlank = false;
                }
            }
        }
    }
}