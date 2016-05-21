using UnityEngine;
using System.Collections;

public class VerticalRaceController : MonoBehaviour {

	private GameController gc;

	public Camera cam;

	public GameObject p1;
	public GameObject p2;

	public GameObject level1;
	public GameObject level2;
	public GameObject level3;

	public int time;

	private bool onlyOnce;

	// Use this for initialization
	void Start () {

		int levelSelect = Random.Range (0, 3); // pick a random level to start with
		if (levelSelect == 0)
			level1.SetActive (true);
		else if (levelSelect == 1)
			level2.SetActive (true);
		else
			level3.SetActive (true);

		cam.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0.5f, 0); // set camera movement speed
		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 3;
	}

	void Update () {

		if (!onlyOnce && gc.gameTime <= 0) {	// make sure you only subtract one life from the loser (false by default)
			onlyOnce = true;

			p1.GetComponentInChildren<JumpChecker>().gameOver = true; // make it impossible for them to move
			p2.GetComponentInChildren<JumpChecker>().gameOver = true; // (mostly so animations dont mess up)
			cam.GetComponent<Rigidbody>().velocity = new Vector3 (0,-1.0f,0);

			if (p1.transform.position.y > p2.transform.position.y + 0.5) { // if someones further than the other, they win
				gc.gameOver (1);
				p1.GetComponent<Animator>().SetTrigger ("Win");
				p2.GetComponent<Animator>().SetTrigger ("Lose");
			}
				
			else if (p1.transform.position.y + 0.5 < p2.transform.position.y) {
				gc.gameOver (2);
				p2.GetComponent<Animator>().SetTrigger ("Win");
				p1.GetComponent<Animator>().SetTrigger ("Lose");
			}
				
			else {
				gc.gameOver (0);
				p1.GetComponent<Animator>().SetTrigger ("Lose");
				p2.GetComponent<Animator>().SetTrigger ("Lose");
			}
		}
	}
}
