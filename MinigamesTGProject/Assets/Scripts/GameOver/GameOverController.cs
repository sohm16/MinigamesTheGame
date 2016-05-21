using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {

	private GameController gc;

	public Text winnerText;
	public Camera mainCamera;

	// Use this for initialization
	void Start () {
	
		gc = GameController.gc;
		gc.timerOverride = true;
		gc.winnerOverride = true;

		winnerText.text = "Player " + gc.gameIsOver.y + " is the CHAMPION!";
		StartCoroutine (animateText ());
	}
	
	private IEnumerator animateText() {

		Vector2 ogPosition = winnerText.transform.position;
		while (true) {
			int translateX = Random.Range(-1,2) * 20; // move the text in X direction by rand -1,0,1 * 20
			int translateY = Random.Range(-1,2) * 20; // in y
			int winColor = Random.Range (0,7); // pick random color for wintext
			winnerText.transform.position = new Vector2 (winnerText.transform.position.x + translateX, winnerText.transform.position.y + translateY);
			yield return new WaitForSeconds(0.08f);
			winnerText.transform.position = ogPosition; // reset position
			yield return new WaitForSeconds (0.01f); // set text to random color
			if (winColor == 0)
				winnerText.color = Color.black;
			else if (winColor == 1)
				winnerText.color = Color.white;
			else if (winColor == 2)
				winnerText.color = Color.cyan;
			else if (winColor == 3)
				winnerText.color = Color.yellow;
			else if (winColor == 4)
				winnerText.color = Color.green;
			else if (winColor == 5)
				winnerText.color = Color.gray;
			else
				winnerText.color = Color.red;
		}
	}
}
