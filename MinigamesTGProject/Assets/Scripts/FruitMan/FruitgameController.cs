using UnityEngine;
using System.Collections;

public class FruitgameController : MonoBehaviour {

	private GameController gc;
	public int time;
	private bool gameOver;

	public GameObject enemies1;
	public GameObject enemies2;
	public GameObject enemies3;
	public GameObject enemies4;

	private int enemyTotal1;
	private int enemyTotal2;
	
	public int enemiesKilled1;
	public int enemiesKilled2;

	// Use this for initialization
	void Start () {
	
		int enemySelector = Random.Range (0, 4);
		if (enemySelector == 0)
			enemies1.SetActive (true);
		else if (enemySelector == 1)
			enemies2.SetActive (true);
		else if (enemySelector == 2)
			enemies3.SetActive (true);
		else
			enemies4.SetActive (true);

		enemyTotal1 = GameObject.FindGameObjectsWithTag ("Enemies1").Length;
		enemyTotal2 = GameObject.FindGameObjectsWithTag ("Enemies2").Length;

		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameObject.FindGameObjectWithTag ("Enemies1") != null) // for hiddenNux: keep track of how many enemies killed
			enemiesKilled1 = enemyTotal1 - GameObject.FindGameObjectsWithTag ("Enemies1").Length;
		if (GameObject.FindGameObjectWithTag ("Enemies2") != null)
			enemiesKilled1 = enemyTotal1 - GameObject.FindGameObjectsWithTag ("Enemies2").Length;


		if (GameObject.FindGameObjectWithTag ("Enemies1") == null && !gameOver) { // if there arent any enemies left on your side
			gameOver = true;	// you win
			gc.overrideTimer = true;
			gc.gameTime = 0;
			enemiesKilled1 = enemyTotal1;
			gc.gameOver (1);
		}
		if (GameObject.FindGameObjectWithTag ("Enemies2") == null && !gameOver) {
			gameOver = true;
			gc.overrideTimer = true;
			gc.gameTime = 0;
			enemiesKilled2 = enemyTotal2;
			gc.gameOver (2);
		}

		if (gc.gameTime <= 0 && !gameOver) { // check how many enemies there are left and whoever has less wins
			gameOver = true;
			if (GameObject.FindGameObjectsWithTag ("Enemies1").Length < GameObject.FindGameObjectsWithTag ("Enemies2").Length)
				gc.gameOver (1);
			else if (GameObject.FindGameObjectsWithTag ("Enemies1").Length > GameObject.FindGameObjectsWithTag("Enemies2").Length)
			    gc.gameOver (2);
			else
				gc.gameOver (0);
		}
	}
}