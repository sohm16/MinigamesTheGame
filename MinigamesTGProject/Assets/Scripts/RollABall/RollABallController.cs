using UnityEngine;
using System.Collections;

public class RollABallController : MonoBehaviour {

	private GameController gc;

	public GameObject p1;
	public GameObject p2;

	public GameObject level1; // for picking a random stage
	public GameObject level2;
	public GameObject level3;

	public int time;

	public int totalPickups;

	private bool onlyOnce;

	// Use this for initialization
	void Start () {
	
		int levelPicker = Random.Range (0, 3); // start with a random level
		if (levelPicker == 0)
			level1.SetActive (true);
		else if (levelPicker == 1)
			level2.SetActive (true);
		else
			level3.SetActive (true);

		gc = GameController.gc;
		gc.startGame (time);
	}
	
	// Update is called once per frame
	void Update () {

		if (p1.GetComponent<BallController>().score + p2.GetComponent<BallController>().score >= totalPickups) {
			gc.overrideTimer = true;
			gc.gameTime = 0;
		}

		if (gc.gameTime <= 0 && !onlyOnce) {

			onlyOnce = true;
			if (p1.GetComponent<BallController> ().score > p2.GetComponent<BallController> ().score)
				gc.gameOver (1);
			else if (p1.GetComponent<BallController> ().score < p2.GetComponent<BallController> ().score)
				gc.gameOver (2);
			else
				gc.gameOver (0);
		}
	}
}
