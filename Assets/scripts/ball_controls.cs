using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_controls : MonoBehaviour {

	public float speed = 1f;
	public float maxspeed = 10f;
	public float rotationSpeed = 5.0f;
	public GameObject playStateController;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = maxspeed;
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			Vector3 force = transform.forward;
			rb.AddForce(force * speed);
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.Rotate(0, -rotationSpeed, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			transform.Rotate(0, rotationSpeed, 0);
		}
	}

}
