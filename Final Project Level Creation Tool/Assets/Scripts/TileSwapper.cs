using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSwapper : MonoBehaviour {
    public TilePlacer tilePlacer;
    public UITile uiTile;

    public GameObject swapperTileA;
    public GameObject swapperTileB;

    private GameObject tileA;
    private GameObject tileB;

    private GameObject tileAParent;
    private GameObject tileBParent;
    
    // Update is called once per frame
    void Update () {
        if(tileA != null && tileB != null) {
            SwapTiles();
        }
	}

    public void ToSwap(GameObject tile) {
        if(tileA == null) {
            tileA = tile;
            tileAParent = tile.transform.parent.gameObject;
        } else {
            tileB = tile;
            tileBParent = tile.transform.parent.gameObject;
        }
    }

    void SwapTiles() {
        swapperTileA.transform.position = tileAParent.transform.position;
        swapperTileB.transform.position = tileBParent.transform.position;

        tileA.transform.position = swapperTileB.transform.position;
        tileB.transform.position = swapperTileA.transform.position;

        swapperTileA.transform.position = new Vector2(10000, 10000);
        swapperTileB.transform.position = new Vector2(10000, 10000);

        tileA.GetComponent<UITile>().BackToBaseSprite();
        tileB.GetComponent<UITile>().BackToBaseSprite();

        tileA.transform.parent.DetachChildren();
        tileB.transform.parent.DetachChildren();

        tileA.transform.SetParent(tileBParent.transform);
        tileB.transform.SetParent(tileAParent.transform);

        tilePlacer.selectedTile = uiTile.tempTile;

        tileA = tileB = null;
    }
}
