using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject cameraGameObject;
    private Vector3 cameraPos;
    private string sizeByXY;

    private TilePlacer tilePlacer;

    public float cameraMovementSpeed = 30.0f;
    public float cameraHeightCurrent;

    public bool invertMovement;

    //Mouse zoom
    public float cameraHeightIncrement;
    private float cameraHeightMin = 20;
    private float cameraHeightMax = 0;

    // Use this for initialization
    void Start() {
        cameraPos = transform.position;
        cameraHeightCurrent = Camera.main.orthographicSize;
        tilePlacer = FindObjectOfType<TilePlacer>();
    }

    // Update is called once per frame
    void Update() {
        if(tilePlacer.xySaved) {
            moveCamera();
            cameraZooming();

            if(cameraHeightCurrent < cameraHeightMin)
                cameraHeightCurrent = cameraHeightMin;

            if(cameraHeightCurrent > cameraHeightMax) {
                cameraHeightCurrent = cameraHeightMax;

                //Move the camera a little bit so that the fully zoomed out level is centered! (Makes things beautifully centered).
                cameraPos.y = (sizeByXY == "X") ? -(tilePlacer.xTiles / 8) : -(tilePlacer.yTiles / 4);
                transform.position = cameraPos;
            }

            cameraMovementSpeed = cameraHeightCurrent * 2;
        }
    }

    //Controls affecting the zoom and placement of the camera
    void cameraZooming() {

        if(cameraHeightMax == 0) {
            cameraHeightMax = SetZoomLimit();
            cameraHeightCurrent = cameraHeightMax;

            //Move the camera a little bit so that the zoomed out blank level is centered! (It looks beautiful, trust me).
            cameraPos.y = (sizeByXY == "X") ? -(tilePlacer.xTiles / 8) : -(tilePlacer.yTiles / 4);
            transform.position = cameraPos;
        }

        //Move camera focus and height based on raycast from maincamera
        RaycastHit hit;
        Vector2 direction = (transform.position - cameraGameObject.transform.position).normalized;

        if(Physics.Raycast(cameraGameObject.transform.position, direction, out hit, 1000.0f)) {
            Debug.DrawLine(cameraGameObject.transform.position, hit.point);

            //Adjust height of the cameraHeight based on difference from focus point
            if(Vector2.Distance(transform.position, cameraGameObject.transform.position) != cameraHeightCurrent) {
                Vector2 newPos = cameraGameObject.transform.position;
                Camera.main.orthographicSize = cameraHeightCurrent;
                cameraGameObject.transform.position = newPos;
            }
        }

        cameraHeightIncrement = cameraHeightCurrent / tilePlacer.tileSize;

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
        if(!FindObjectOfType<TilePlacer>().levelNameIF.isFocused) {
            cameraMovementSpeed = invertMovement ? -Mathf.Abs(cameraMovementSpeed) : Mathf.Abs(cameraMovementSpeed);

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

        if(((float)tilePlacer.xTiles / (float)tilePlacer.yTiles) >= 2.5f) {
            temp = tilePlacer.xTiles * 1.25f;
            sizeByXY = "X";
        } else {
            temp = tilePlacer.yTiles * 3.5f;
            sizeByXY = "Y";
        }
        return temp;
    }
}