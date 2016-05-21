using UnityEngine;
using System.Collections;

public class FastRunnerController : MonoBehaviour {

	private GameController gc;

	public Player player1;
	public Player player2;

	public GameObject mainCamera;
	public float scrollSpeed;

	public int time;

	public float fallBehindDistance = 14.5f;
	public float fallDownDistance = -9.5f;
	public float victoryDistance = 184.5f;

	private bool onlyOnce;
	private int gameOverState;

	void Start () {
		gc = GameController.gc;
		gc.startGame (time);
		gameOverState = -1;
		onlyOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gc.gameTime > 0) {
			mainCamera.transform.Translate (scrollSpeed * Time.deltaTime, 0f, 0f);
		} else if (!onlyOnce && gc.gameTime <= 0) {
			player1.stop = player2.stop = true;
			onlyOnce = true;
			gameOverState = 0;
			if (player1.transform.position.x > player2.transform.position.x + 0.2f)
				gameOverState = 1;
			if (player2.transform.position.x > player1.transform.position.x + 0.2f)
				gameOverState = 2;
			gc.gameOver (gameOverState);
		}

		float fellBehind = mainCamera.transform.position.x - fallBehindDistance;

		if (player1.transform.position.x < fellBehind || player1.transform.position.y < fallDownDistance || player2.transform.position.x > victoryDistance) {
			gameOverState = 2;
		}

		if (player2.transform.position.x < fellBehind || player2.transform.position.y < fallDownDistance || player1.transform.position.x > victoryDistance) {
			gameOverState = 1;
		}

		if (!onlyOnce && gameOverState >= 0) {
			player1.stop = player2.stop = true;

			if (!(player1.transform.position.y < fallDownDistance || player2.transform.position.y < fallDownDistance)) {
				if (Mathf.Abs (player1.transform.position.x - player2.transform.position.x) < 0.2f)
					gameOverState = 0;
			} else {
				if (Mathf.Abs (player1.transform.position.y - player2.transform.position.y) < 0.2f)
					gameOverState = 0;
			}

			gc.overrideTimer = true;
			gc.gameTime = 0;
			gc.gameOver (gameOverState);
			onlyOnce = true;
		}


	}
}
