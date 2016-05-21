using UnityEngine;
using System.Collections;

public class CrabController : MonoBehaviour {

	public float speed;
	public Boundary boundary;

	public Vector3[] localWaypoints;
	private Vector3[] globalWaypoints;

	public float easeAmount = .75f;

	int fromWaypointIndex;
	float percentBetweenWaypoints;
	int nextWaypointIndex;

	private CrabShooterController crabShooterController;
	private GameController gc;

	private bool gameIsOver;

	// Use this for initialization
	void Start () {
		gc = GameController.gc;

		globalWaypoints = new Vector3[localWaypoints.Length];
		for (int i=0; i < localWaypoints.Length; i++) {
			globalWaypoints[i] = localWaypoints[i] + transform.position;
		}

		SetNextWaypoint ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.loadedLevelName == "Enemy Crab Shooter" && gc.gameTime == 0 && !gameIsOver)
			gc.gameOver (0);

		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints [fromWaypointIndex], globalWaypoints [nextWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
		percentBetweenWaypoints = Mathf.Clamp01 (percentBetweenWaypoints);
		float easedPercentBetweenWaypoints = Ease (percentBetweenWaypoints);
		
		Vector3 newPos = Vector3.Lerp (globalWaypoints [fromWaypointIndex], globalWaypoints [nextWaypointIndex], easedPercentBetweenWaypoints);

		if (percentBetweenWaypoints >= 1) {
			percentBetweenWaypoints = 0;
			fromWaypointIndex = nextWaypointIndex;

			SetNextWaypoint();
		}

		transform.Translate (newPos - transform.position);

		//transform.Translate (new Vector3 (speed, 0f, 0f) * Time.deltaTime);

		/*if (transform.position.x < boundary.xMin) {
			speed = -speed;
			transform.position = new Vector3 (boundary.xMin, transform.position.y, 0f);
		}

		if (transform.position.x > boundary.xMax) {
			speed = -speed;
			transform.position = new Vector3 (boundary.xMax, transform.position.y, 0f);
		}*/

	}

	float Ease (float x) {
		float a = easeAmount + 1;
		return Mathf.Pow (x, a) / (Mathf.Pow (x, a) + Mathf.Pow (1 - x, a));
	}
	
	void OnTriggerEnter2D (Collider2D other) {

		if (Application.loadedLevelName == "Enemy Crab Shooter") { // for credits

			if (other.tag == "player1") {
				Destroy (gameObject);
				gc.overrideTimer = true;
				gc.gameTime = 0;
				gameIsOver = true;
				gc.gameOver (1);
			}
			if (other.tag == "player2") {
				Destroy (gameObject);
				gc.overrideTimer = true;
				gc.gameTime = 0;
				gameIsOver = true;
				gc.gameOver (2);
			}
		}
	}

	void OnDrawGizmos () {
		if (localWaypoints != null) {
			Gizmos.color = Color.cyan;
			float size = 0.4f;

			for (int i =0; i < localWaypoints.Length; i++) {
				Vector3 globalWaypointPos = localWaypoints[i] + transform.position;
				Gizmos.DrawLine (globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine (globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
			}
		}
	}

	void SetNextWaypoint () {
		while (fromWaypointIndex == nextWaypointIndex) {
			nextWaypointIndex = (int) Random.Range (0, globalWaypoints.Length);
		}

//		Debug.Log (nextWaypointIndex);
	}
}
