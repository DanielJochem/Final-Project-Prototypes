using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fix sprite rotations so that they face the camera, making the world look 3D.
public class RotationFix : MonoBehaviour {
    
    private void Start() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward) * Quaternion.Euler(0f, 0f, -45f);
    }
}