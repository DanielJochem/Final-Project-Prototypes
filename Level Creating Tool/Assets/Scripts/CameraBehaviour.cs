using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    //Mouse Scrolling
    public float cameraMovementSpeed = 30.0f;

    public bool invertMovement;

    //Mouse zoom
    public float cameraHeightCurrent;
    public float cameraHeightIncrement;
    public float cameraHeightMin;
    public float cameraHeightMax;

    public GameObject cameraGO;

    // Use this for initialization
    void Start() {
        cameraHeightCurrent = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update() {
        moveCamera();
        cameraZooming();

        if(cameraHeightCurrent < cameraHeightMin)
            cameraHeightCurrent = cameraHeightMin;

        if(cameraHeightCurrent > cameraHeightMax)
            cameraHeightCurrent = cameraHeightMax;
    }

    //Controls affecting the zoom and placement of the camera
    void cameraZooming() {
        //Move camera focus and height based on raycast from maincamera
        RaycastHit hit;
        Vector2 direction = (transform.position - cameraGO.transform.position).normalized;

        if(Physics.Raycast(cameraGO.transform.position, direction, out hit, 1000.0f)) {
            Debug.DrawLine(cameraGO.transform.position, hit.point);

            //Adjust height of the cameraHeight based on difference from focus point
            if(Vector2.Distance(transform.position, cameraGO.transform.position) != cameraHeightCurrent) {
                Vector2 newPos = cameraGO.transform.position;
                Camera.main.orthographicSize = cameraHeightCurrent;
                cameraGO.transform.position = newPos;
            }
        }

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
        if(invertMovement) {
            if(Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(Vector2.right * cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.RightArrow)) {
                transform.Translate(Vector2.right * -cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(transform.up * cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(transform.up * -cameraMovementSpeed * Time.deltaTime);
            }
        } else {
            if(Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(Vector2.right * -cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.RightArrow)) {
                transform.Translate(Vector2.right * cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(transform.up * -cameraMovementSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(transform.up * cameraMovementSpeed * Time.deltaTime);
            }
        }
    }
}
