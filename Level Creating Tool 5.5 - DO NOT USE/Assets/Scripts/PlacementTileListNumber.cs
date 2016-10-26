using UnityEngine;

public class PlacementTileListNumber : MonoBehaviour {
    public int listNum;
    private bool tileSet;
    public bool isBlank;
    private TilePlacer tilePlacer;

    void Start() {
        tilePlacer = FindObjectOfType<TilePlacer>();
        isBlank = true;
    }

    void OnMouseOver() {
        if(Input.GetMouseButton(0) && !tileSet) {
            tileSet = true;
            tilePlacer.OnTileClicked(gameObject);
        }
    }

    void OnMouseExit() {
        tileSet = false;
    }

    void OnMouseUp() {
        tileSet = false;
    }
}
