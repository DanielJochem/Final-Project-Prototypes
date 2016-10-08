using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour {

    public Transform target;

    //The Distance and Height from the object.
    public float distance = 20.0f;
    public float height = 5.0f;

    public Vector3 lookAtVector;

    // Use this for initialization
    void Start () {
        Camera.main.orthographicSize = 10;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
