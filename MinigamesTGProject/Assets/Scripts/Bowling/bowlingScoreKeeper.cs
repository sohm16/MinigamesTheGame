using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bowlingScoreKeeper : MonoBehaviour {
	public GameObject[] pins1;
	public GameObject[] pins2;
	public Text score1Text;
	public Text score2Text;
	public Text strikeText1;
	public Text strikeText2;
	public int time;

	public int score1;
	public int score2;
	private bool onlyOnce1;
	private bool onlyOnce2;
	private bool onlyOnce3;
	private Quaternion rotation; 
	private GameController gc;
	private bowlingBallController bbc1;
	private bowlingBallController bbc2;
	// Use this for initialization
	void Start () {
		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 5f;
		rotation = pins1 [0].transform.rotation;
		bbc1 = GameObject.Find("ball").GetComponent<bowlingBallController>();
		bbc2 = GameObject.Find("ball2").GetComponent<bowlingBallController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (bbc1.ballLaunched && !onlyOnce1) {
			onlyOnce1 = true;
			StartCoroutine(WaitForBall1());
		}
		if (bbc2.ballLaunched && !onlyOnce2) {
			onlyOnce2 = true;
			StartCoroutine(WaitForBall2());
		}


		if ((gc.gameTime <= 0 && !onlyOnce3) || (bbc1.ballLaunched && bbc2.ballLaunched && !onlyOnce3)) {
			onlyOnce3 = true;
			StartCoroutine(declareWinner());
		}


	}


	IEnumerator WaitForBall1()
	{
		yield return new WaitForSeconds(3.5f);
		foreach(GameObject pin in pins1){
			float angle = Quaternion.Angle(pin.transform.rotation, rotation);
			if (angle > 30)
				score1++;
		}

		score1Text.text = "Pins: " + score1;
		if (score1 >= 10)
			strikeText1.text = "Strike!";
		
	}

	IEnumerator WaitForBall2(){
		yield return new WaitForSeconds(3.5f);
		foreach(GameObject pin in pins2){
			float angle = Quaternion.Angle(pin.transform.rotation, rotation);
			if (angle > 30)
				score2++;
		}
		score2Text.text = "Pins: " + score2;
		if (score2 >= 10)
			strikeText2.text = "Strike!";
	}

	IEnumerator declareWinner()
	{
		yield return new WaitForSeconds (3.6f);
		if(score1 > score2)
			gc.gameOver(1);
		else if(score1 < score2)
			gc.gameOver(2);
		else if(score1 == score2)
			gc.gameOver(0);
	}
}
