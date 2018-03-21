using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {

	public Vector3 cueOffset;
	public Vector3 ballOffset;
	public GameObject cue;
	public GameObject ball;

	public float smooth = 0.125f;

	private Transform target;
	private Vector3 offset;
	private Vector3 velocity = Vector3.zero;
	private play_state_controller.PlayState playState;
	
	void Start() {
		ChangeTarget(play_state_controller.PlayState.cue);
	}

	void Update () {
		Vector3 desiredPosition = target.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smooth);
		// if (playState == play_state_controller.PlayState.ball)
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smooth);
		if (playState == play_state_controller.PlayState.cue){
			transform.LookAt(ball.transform, ball.transform.up);
		}
	}

	public void ChangeTarget(play_state_controller.PlayState newTarget) {
		playState = newTarget;
		switch(newTarget) {
			case play_state_controller.PlayState.cue: {
				target = cue.transform;
				offset = cueOffset;
				break;
			}
			case play_state_controller.PlayState.ball: {
				target = ball.transform;
				offset = ballOffset;
				break;
			}
		}
	}
}
