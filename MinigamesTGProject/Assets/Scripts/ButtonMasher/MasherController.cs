using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MasherController : MonoBehaviour {

	private GameController gc;

	public Text p1Score;
	public Text p2Score;

	public int time;

	public int p1ScoreVal;
	public int p2ScoreVal;

	private bool onlyOnce; // put this in every minigame along with score check at the bottom

	// Use this for initialization
	void Start () { // Make sure you use Start, not Awake (so Loader goes first if necessary)
	
		gc = GameController.gc; // get GameController
		gc.startGame (time); // start game with 5 seconds on the clock
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		p1ScoreVal = p1Score.GetComponent<ButtonMashPlayer> ().myScore; // show who's winning
		p2ScoreVal = p2Score.GetComponent<ButtonMashPlayer> ().myScore;
		if (p1ScoreVal > p2ScoreVal && !onlyOnce) {
			p1Score.color = Color.green;
			p2Score.color = Color.red;
		} 
		else if (p1ScoreVal < p2ScoreVal && !onlyOnce) {
			p1Score.color = Color.red;
			p2Score.color = Color.green;
		} 
		else if (!onlyOnce) {
			p1Score.color = Color.white;
			p2Score.color = Color.white;
		}

		if (gc.gameTime <= 0 && !onlyOnce) { // as long as game is running
	// make sure you only subtract one life from the loser (false by default)

			onlyOnce = true;

			p1ScoreVal = p1Score.GetComponent<ButtonMashPlayer> ().myScore;
			p2ScoreVal = p2Score.GetComponent<ButtonMashPlayer> ().myScore;

			if (p1ScoreVal > p2ScoreVal) {
				gc.gameOver (1);
				StartCoroutine (winFlash (p1Score));
			}

			else if (p1ScoreVal < p2ScoreVal) {
				gc.gameOver (2);
				StartCoroutine (winFlash (p2Score));
			}

			else
				gc.gameOver (0);
		}
	}

	private IEnumerator winFlash(Text winner) {

		while (true) {
			winner.color = Color.white;
			yield return new WaitForSeconds (0.2f);
			winner.color = Color.green;
			yield return new WaitForSeconds (0.2f);
		}
	}
}