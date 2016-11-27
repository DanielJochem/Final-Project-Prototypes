using UnityEngine;

public class TileListNumber : MonoBehaviour {
    private PlayerMovement playerMovement;

	public int listNum;


    void Start() {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


	public void OnMouseUp() {
        if(playerMovement.direction == "") {
            //Debug.Log("I am List Number: " + listNum + ". My Position is: " + gameObject.transform.GetChild(0).GetChild(0).transform.position);
            playerMovement.wantedTileNumber = listNum;
        }
	}
}
