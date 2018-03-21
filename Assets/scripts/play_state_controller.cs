using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_state_controller : MonoBehaviour {
	public enum PlayState {
		unset,
		cue,
		ball
	}
	private PlayState playState;

	public GameObject cam;
	public GameObject ball;
	public GameObject ballParent;
	public GameObject cue;

	private ArrayList balls;
     
	void Start() {
		SetPlayState(PlayState.cue);
	}

	void Update() {
		if (balls == null) {
			generate_balls gb = ballParent.GetComponent<generate_balls>();
			balls = gb.GetBalls();
		}
		//Determine if next turn ready
		if (playState == PlayState.ball) {
			float movement = 0;
			Rigidbody wBallRb = ball.GetComponent<Rigidbody>();
			movement += wBallRb.velocity.magnitude;
			foreach (GameObject b in balls) {
				Rigidbody ballRb = b.GetComponent<Rigidbody>();
				movement += ballRb.velocity.magnitude;
			}
			if (movement == 0) StartNextTurn();
		}
	}

	public bool SetPlayState(PlayState state) {
		if (state == playState) return false;
		camera_controller cc = cam.GetComponent<camera_controller>();

		playState = state;
		cc.ChangeTarget(playState);
		if (state == PlayState.ball) {
			ball.GetComponent<ball_controls>().enabled = true;
			ball.GetComponent<MeshRenderer>().enabled = false;
			cue.GetComponent<CapsuleCollider>().enabled = false;
		} else if (state == PlayState.cue) {
			cue.GetComponent<CapsuleCollider>().enabled = true;
			ball.GetComponent<ball_controls>().enabled = false;
			ball.GetComponent<MeshRenderer>().enabled = true;
		}
		return true;
	}

	public PlayState GetPlayState() { return playState; }

	private void StartNextTurn() {
		Debug.Log("Allons-y");
		SetPlayState(PlayState.cue);
		cue_controls cc = cue.GetComponent<cue_controls>();
		cc.CenterCueOnBall();

	}
}
