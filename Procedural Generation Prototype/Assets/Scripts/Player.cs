using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody rb;
	Vector3 velocity;
    public float speed;
    private Vector3 rot;
	
	void Start() {
		rb = GetComponent<Rigidbody>();
        rot = rot;
	}

	void Update() {
		velocity = new Vector3 (0, 0, Input.GetAxisRaw ("Vertical")).normalized * 10;
	}

	void FixedUpdate() {
        //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        //transform.position = (transform.position + velocity) * Time.fixedDeltaTime;
        transform.Translate(velocity * Time.fixedDeltaTime);

        //if(rot.y > 178 && rot.y < 182 || rot.y > 358 || rot.y < 2) {
        //    rot.y = rot.y - 3;
        //}

        if(Input.GetAxisRaw("Horizontal") != 0) {
            transform.Rotate(new Vector3(0, rot.y + Input.GetAxisRaw("Horizontal"), 0).normalized * 2 * speed);
        }


        //    rot *= Quaternion.Euler(0, (Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime), 0);
        //}
    }
}