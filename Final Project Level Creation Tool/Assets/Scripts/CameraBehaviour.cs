using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	[SerializeField]
    private GameObject cameraGameObject;
    private Vector3 cameraPos;
    private string sizeByXY;

    private UIRelatedStuff uiRelatedStuff;

	[SerializeField]
    private float cameraMovementSpeed = 30.0f, cameraHeightCurrent;

	[SerializeField]
    private bool invertMovement;

    //Mouse zoom
	[SerializeField]
    public float cameraHeightIncrement, cameraHeightMin = 20, cameraHeightMax = 0;

    // Use this for initialization
    void Start() {
        cameraPos = transform.position;
        cameraHeightCurrent = Camera.main.orthographicSize;
        uiRelatedStuff = FindObjectOfType<UIRelatedStuff>();
    }
	
    void Update() {
        if(uiRelatedStuff.xySaved) {
            moveCamera();
            cameraZooming();

            if(cameraHeightCurrent < cameraHeightMin)
                cameraHeightCurrent = cameraHeightMin;

            if(cameraHeightCurrent > cameraHeightMax) {
                cameraHeightCurrent = cameraHeightMax;

                //Move the camera a little bit so that the fully zoomed out level is centered! (Makes things beautifully centered).
                cameraPos.y = (sizeByXY == "X") ? -(uiRelatedStuff.xTiles / 8) : -(uiRelatedStuff.yTiles / 4);
                transform.position = cameraPos;
            }

            cameraMovementSpeed = cameraHeightCurrent * 2;
        }
    }

    //Controls affecting the zoom and placement of the camera
    public void cameraZooming() {

        if(cameraHeightMax == 0) {
            cameraHeightMax = SetZoomLimit();
            cameraHeightCurrent = cameraHeightMax;

            //Move the camera a little bit so that the zoomed out blank level is centered! (It looks beautiful, trust me).
            cameraPos.y = (sizeByXY == "X") ? -(uiRelatedStuff.xTiles / 8) : -(uiRelatedStuff.yTiles / 4);
            transform.position = cameraPos;
        }

        cameraHeightIncrement = cameraHeightCurrent / uiRelatedStuff.tileSize;

        //Adjust Camera Height - Scrollwheel
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && cameraHeightCurrent > cameraHeightMin) {
            cameraHeightCurrent -= cameraHeightIncrement;
        } else if(Input.GetAxis("Mouse ScrollWheel") < 0 && cameraHeightCurrent < cameraHeightMax) {
            cameraHeightCurrent += cameraHeightIncrement;
        }

        Camera.main.orthographicSize = cameraHeightCurrent;
    }

    //Move Camera using mouse
    void moveCamera() {
        if(!uiRelatedStuff.levelNameIF.isFocused) {
            cameraMovementSpeed = invertMovement ? Mathf.Abs(cameraMovementSpeed) : -Mathf.Abs(cameraMovementSpeed);

            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                transform.Translate(Vector2.right * cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
                transform.Translate(Vector2.right * -cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                transform.Translate(transform.up * cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                transform.Translate(transform.up * -cameraMovementSpeed * Time.deltaTime);
            }
        }
    }

    float SetZoomLimit() {
        float temp;

        if(((float)uiRelatedStuff.xTiles / (float)uiRelatedStuff.yTiles) >= 2.5f) { //or 0.86f
            temp = uiRelatedStuff.xTiles * 1.25f; //or 5f
            sizeByXY = "X";
        } else {
            temp = uiRelatedStuff.yTiles * 3.5f;
            sizeByXY = "Y";
        }
        return temp;
    }
}