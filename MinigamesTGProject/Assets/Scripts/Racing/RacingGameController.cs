using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RacingGameController : MonoBehaviour {
	public int score1;
	public int score2;
	public Text score1Text;
	public Text score2Text;
	public int time;
	public static RacingGameController instance = null;
	
	private bool onlyOnce;
	private GameController gc;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject); 
	}

	void Start () {
		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 2;

	}
	
	// Update is called once per frame
	void Update () {

		if(gc.gameTime <= 0 && !onlyOnce){
			
			onlyOnce = true;
			if (score1 > score2)
				gc.gameOver (1);
			else if (score1 < score2) 
				gc.gameOver (2);
			else if (score1 == score2)
				gc.gameOver (0);
		}
	}


	public void updateText(){
		score1Text.text = "Player 1 Score: " + score1;
		score2Text.text = "Player 2 Score: " + score2;
	}
}
