using UnityEngine;
using System.Collections;

public class ClimberController : MonoBehaviour {

	private GameController gc;

	public Player player1;
	public Player player2;

	public GameObject mainCamera;
	public float scrollSpeed;

	public int time;

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
			mainCamera.transform.Translate (0f, scrollSpeed * Time.deltaTime, 0f);
		}else if (!onlyOnce && gc.gameTime <=0 ) {
			player1.stop = player2.stop = true;
			onlyOnce = true;
			gameOverState = 0;
			if (player1.transform.position.y > player2.transform.position.y + 0.2f)
				gameOverState = 1;
			if (player2.transform.position.y > player1.transform.position.y + 0.2f)
				gameOverState = 2;
			gc.gameOver (gameOverState);
		}

		if (player1.transform.position.y < mainCamera.transform.position.y - 12f)
			gameOverState = 2;

		if (player2.transform.position.y < mainCamera.transform.position.y - 12f)
			gameOverState = 1;

		if (player1.transform.position.y > 86f)
			gameOverState = 1;

		if (player2.transform.position.y > 86f)
			gameOverState = 2;

		if (!onlyOnce && gameOverState >= 0) {
			player1.stop = player2.stop = true;

			if (Mathf.Abs (player1.transform.position.y - player2.transform.position.y) < 0.2f)
				gameOverState = 0;

			gc.overrideTimer = true;
			gc.gameTime = 0;
			gc.gameOver (gameOverState);
			onlyOnce = true;
		}

	}
}
