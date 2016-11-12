using UnityEngine;

public class PlacementTileListNumber : MonoBehaviour {
    public int listNum;
    public bool isBlank;

    private bool tileSet;
    private TilePlacer tilePlacer;

    void Start() {
        tilePlacer = FindObjectOfType<TilePlacer>();
        isBlank = true;
    }

    void OnMouseOver() {
        if((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !tileSet) {
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
