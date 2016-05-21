using UnityEngine;
using System.Collections;

public class BoundaryEnder : MonoBehaviour {

	private GameController gc;
	private bool onlyOnce;

	public GameObject player1;
	public GameObject player2;

	// Use this for initialization
	void Start () {
	
		gc = GameController.gc;
	}

	void OnTriggerEnter(Collider other) { // if theres a player thru the boundary, other player wins

		if (other.gameObject.tag == "Player" && !onlyOnce && !gc.timerOverride && !player1.GetComponentInChildren<JumpChecker>().gameOver && !player2.GetComponentInChildren<JumpChecker>().gameOver) {
			onlyOnce = true;
			gc.overrideTimer = true;
			gc.gameTime = 0;
			player2.GetComponentInChildren<JumpChecker>().gameOver = true; // make sure players cant move
			player1.GetComponentInChildren<JumpChecker>().gameOver = true;

			if (other.gameObject == player1) {
				gc.gameOver (2);
				player2.GetComponent<Animator>().SetTrigger("Win");
			}
			else {
				gc.gameOver(1);
				player1.GetComponent<Animator>().SetTrigger("Win");
			}
		}
	}
}
