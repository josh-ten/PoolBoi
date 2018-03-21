using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cue_controls : MonoBehaviour {

	public bool mousePressed = false;
	private Vector2 firstClick;
	private int cueState = 0;
	private bool cueStateChosen = false;
	
	private Rigidbody rb;
	public float hitSensitivity = 1f;

	public GameObject ball;
	public float rotSensitivity = 0.3f;

	public Vector3 cueDistFromBall = Vector3.zero;
	private Quaternion defaultRot;

	void Start () {
		rb = GetComponent<Rigidbody>();
		defaultRot = transform.rotation;
	}
	
	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0)) {
			firstClick = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
			mousePressed = true;
		else {
			mousePressed = false;
			cueStateChosen = false;
		}
		if (mousePressed) {
			Vector2 currentPos = Input.mousePosition;
			Vector2 difference = firstClick - currentPos;
			//Choose cue state based on direction of line drawn
			if (!cueStateChosen && difference.magnitude > 2) {
				if (Mathf.Abs(difference.x) >= Mathf.Abs(difference.y)) cueState = 0;
				else cueState = 1;
				cueStateChosen = true;
			}
		
			if (cueState == 0) {
				//Aim
				Vector3 ballPos = ball.transform.position;
				transform.RotateAround(ballPos, Vector3.up, difference.x * rotSensitivity);
			} else if (cueState == 1) {
				//Hit
				Vector3 dirToBall = transform.position - ball.transform.position;
				dirToBall.Normalize();
				Vector3 force = dirToBall * difference.y * hitSensitivity;
				rb.AddForce(force);
			}

			firstClick = Input.mousePosition;
		}
	}

	public void CenterCueOnBall() {
		Vector3 ballPos = ball.transform.position;
		transform.rotation = defaultRot;
		transform.position = ballPos + cueDistFromBall;
	}
}
