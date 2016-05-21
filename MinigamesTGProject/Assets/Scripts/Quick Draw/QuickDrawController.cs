using UnityEngine;
using System.Collections;

public class QuickDrawController : MonoBehaviour {

	private GameController gc;

	public Camera cam;

	public GameObject p1;
	public GameObject p2;
	public GameObject crenshaw;
	public GameObject crenshawBubble;

	public int time;

	private bool onlyOnce;

	public bool readyDraw = false;
	public bool p1Fired = false;
	public bool p2Fired = false;

	public float drawTime;

	public Sprite vegdahlWait;
	public Sprite vegdahlFire;
	public Sprite nuxWait;
	public Sprite nuxFire;
	public Sprite crenshawWait;
	public Sprite crenshawFire;
	public Sprite drawSign;
	public Sprite vegTooSoon;
	public Sprite nuxTooSoon;
	public Sprite vegFiredWon;
	public Sprite nuxFiredWon;

	public bool draw1; // for hiddenNux
	public bool draw2;
	public bool draw3;
	public bool draw4;

	// Use this for initialization
	void Start () {

		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 3;

		int timeRandGen = Random.Range(1,5);

		//randomly chooses how long to wait before drawing between 4 choices
		if (timeRandGen == 1)
			drawTime = 35f;
		else if (timeRandGen == 2)
			drawTime = 40f;
		else if (timeRandGen == 3)
			drawTime = 30f;
		else 
			drawTime = 45f;

	}

	void Update () {

		//this will serve the game over conditions
		if (!onlyOnce && gc.gameTime <= 0) {	// make sure you only subtract one life from the loser (false by default)
			onlyOnce = true;

			if (readyDraw && p1Fired) {

				p1.GetComponent<SpriteRenderer>().sprite = nuxFire;
				crenshawBubble.GetComponent<SpriteRenderer>().sprite = nuxFiredWon;
				draw1 = true;
				gc.gameOver (1);
			}
				
			else if (readyDraw && p2Fired) {
				p2.GetComponent<SpriteRenderer>().sprite = vegdahlFire;
				crenshawBubble.GetComponent<SpriteRenderer>().sprite = vegFiredWon;
				draw2 = true;
				gc.gameOver (2);
			}

			else if (!readyDraw && p2Fired) {
				p2.GetComponent<SpriteRenderer>().sprite = vegdahlFire;
				crenshawBubble.GetComponent<SpriteRenderer>().sprite = vegTooSoon;
				draw3 = true;
				gc.gameOver (1);
			}

			else if (!readyDraw && p1Fired) {
				p1.GetComponent<SpriteRenderer>().sprite = nuxFire;
				crenshawBubble.GetComponent<SpriteRenderer>().sprite = nuxTooSoon;
				draw4 = true;
				gc.gameOver (2);
			}

			else {
				gc.gameOver (0);
			}
		}

		//this will trigger Crenshaw's DRAW! sign
		if (gc.gameTime < drawTime && readyDraw == false) {
			readyDraw = true;
			crenshaw.GetComponent<SpriteRenderer>().sprite = crenshawFire;

			if(gc.gameTime != 0)
			crenshawBubble.GetComponent<SpriteRenderer>().sprite = drawSign;
		}

		//this sees who fires when
		if (Input.GetKey (KeyCode.T) || Input.GetKey("joystick 1 button 0")) {
			p1Fired = true;
			gc.gameTime = 0;
			gc.overrideTimer = true;
		}

		if (Input.GetKey (KeyCode.Keypad0) || Input.GetKey (KeyCode.Alpha0) || Input.GetKey("joystick 2 button 0")) {
			p2Fired = true;
			gc.gameTime = 0;
			gc.overrideTimer = true;
		}

	}
}
