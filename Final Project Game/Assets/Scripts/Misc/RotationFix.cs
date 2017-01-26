using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour {
    
    private void Start() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward) * Quaternion.Euler(0f, 0f, -45f);
    }
}