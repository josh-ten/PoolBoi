using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_balls : MonoBehaviour {

	public Vector3 frontBallPosition = new Vector3(-10, 0.5f, 0);
	public int number_of_rows;
	public GameObject ball;
	public Material[] materials;

	private ArrayList balls;

	void Start () {
		balls = new ArrayList();
		GenerateBalls(number_of_rows);
	}
	
	bool GenerateBalls(int rows) {
		int numInRow = 1;
		int counter = 0;
		Vector3 startPos = frontBallPosition;
		balls.Clear();
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < numInRow; j++) {
				//Set Position
				float xOff = i * -0.9f;
				float startZOff = (numInRow-1) * -0.5f;
				float zOff = startZOff + j;
				Vector3 offset = new Vector3(xOff, 0f, zOff);
				Vector3 position = startPos + offset;
				GameObject newBall = GameObject.Instantiate(ball, position, Quaternion.identity);
				newBall.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
				newBall.name = "ball_" + counter;
				
				newBall.transform.parent = gameObject.transform;
				balls.Add(newBall);
				counter++;
			}
			numInRow++;
		}
		return true;
	}

	public ArrayList GetBalls() { return balls; }
}
