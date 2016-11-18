using UnityEngine;

public class PlacementTileListNumber : MonoBehaviour {

	[HideInInspector]
	public int listNum;

	//[HideInInspector]
	public bool isBlank;

    private bool tileSet;
    private TilePlacer tilePlacer;
	private LevelLoader levelLoader;

	void Awake() {
        tilePlacer = FindObjectOfType<TilePlacer>();
		levelLoader = FindObjectOfType<LevelLoader>();

		if(!levelLoader.isFromLevelLoader) {
			isBlank = true;
		}
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
