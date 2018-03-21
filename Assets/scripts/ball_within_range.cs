using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_within_range : MonoBehaviour {

	private float suction = 1f;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "ball") {
			Rigidbody otherRb = collider.gameObject.GetComponent<Rigidbody>();
			Vector3 difference = collider.transform.position - transform.position;
			otherRb.AddForce(difference * -suction, ForceMode.Impulse);
			Destroy(collider.gameObject, 0.5f);
		}
	}
}
