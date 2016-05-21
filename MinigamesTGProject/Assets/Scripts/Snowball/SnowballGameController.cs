using UnityEngine;
using System.Collections;

public class SnowballGameController : MonoBehaviour {
	
	public GameObject p1;
	public GameObject p2;

	public bool collided; // for hiddenNux

	public int time;

	private float p1Dist;
	private float p2Dist;

	private bool onlyOnce;

	private GameController gc;

	// Use this for initialization
	void Start () {
	
		Physics.IgnoreCollision (p1.GetComponent<Collider> (), p2.GetComponent<Collider> ()); // ignore other player

		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (gc.gameTime <= 0 && !onlyOnce) {
		
			onlyOnce = true;
			p1Dist = p1.transform.position.z; // get their z coordinates in the frame when timer hits 0
			p2Dist = p2.transform.position.z;

			if (p1Dist > p2Dist + 0.2f) // check who's farther along, small buffer
				gc.gameOver (1);
			else if (p1Dist + 0.2f < p2Dist)
				gc.gameOver (2);
			else
				gc.gameOver (0);
		}
	
	}

	void OnTriggerEnter(Collider other) { // if a player passes the finish line

		if (other.gameObject == p1 && !onlyOnce) {
			collided = true;
			onlyOnce = true;
			gc.gameOver (1);
			gc.overrideTimer = true;
			gc.gameTime = 0;
		} 
		else if (other.gameObject == p2 && !onlyOnce) {
			collided = true;
			onlyOnce = true;
			gc.gameOver (2);
			gc.overrideTimer = true;
			gc.gameTime = 0;
		}
	}
}
