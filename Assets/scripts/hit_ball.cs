using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_ball : MonoBehaviour {

	public float forceMultiplier = 2;
	public float maxForce = 10;
	public GameObject whiteBall;
	public GameObject playStateController; //play_state_controller
	private play_state_controller psc;
	private Rigidbody ballRb;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		ballRb = whiteBall.GetComponent<Rigidbody>();
		rb = GetComponent<Rigidbody>();
		psc = playStateController.GetComponent<play_state_controller>();
	}

	void OnCollisionEnter(Collision collider) {
		if (collider.gameObject == whiteBall) {
			GetComponent<cue_controls>().mousePressed = false;
			GetComponent<CapsuleCollider>().enabled = false;
			//Make the cue fly up after a shot
			rb.AddForce(0f, 20f, 0f, ForceMode.Impulse);
			rb.AddTorque(0f, 0f, 50f, ForceMode.Impulse);
			//Hit the ball
			Vector3 force = collider.relativeVelocity * -forceMultiplier;
			force = MaxMag(force, maxForce);
			ballRb.AddForce(force, ForceMode.Impulse);
			psc.SetPlayState(play_state_controller.PlayState.ball);
		}
	}

	Vector3 MaxMag(Vector3 vector, float maxMag) {
		if (vector.magnitude < maxMag) return vector;
		Vector3 output = vector.normalized;
		return output * maxMag;
	}
}
