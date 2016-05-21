using UnityEngine;
using System.Collections;

public class FruitblockController : MonoBehaviour {

	private GameController gc;

	public GameObject level1;
	public GameObject level2;
	public GameObject level3;

	public int time1;
	public int time2;
	public int time3;

	// Use this for initialization
	void Start () {

		gc = GameController.gc;
		int levelPicker = Random.Range (0, 3); // randomly pick a level at the start
		if (levelPicker == 0) {
			level1.SetActive (true);
			gc.startGame (time1);
		} 
		else if (levelPicker == 1) {
			level2.SetActive (true);
			gc.startGame (time2);
		} 
		else {
			level3.SetActive (true);
			gc.startGame (time3);
		}
	}

	void Update () {

		if (gc.gameIsOver.x == 0 && gc.gameIsOver.y == 0 && gc.gameTime <= 0 && !gc.overrideTimer) // if a player hasn't already
			gc.gameOver (0); // hit the switch and the time is out, end the game
	}
}
