using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController gc = null;

	// public vars

	public Text rounds; // GUI in project
	public Text livesNumP1;
	public Text livesNumP2;
	public Text winnerText;

	private AudioSource aSource;
	public AudioClip retroWin;
	public AudioClip somberFanFare;
	public AudioClip superFanFare;
	public AudioClip quickFailure;
	public AudioClip gunShot;
	public AudioClip booSound;

	public Texture2D timeFullTex;
	public Texture2D timeEmptyTex; 
	private Vector2 timePos = new Vector2(Screen.width / 3, Screen.height - Screen.height / 10);// = new Vector2(0,Screen.height - Screen.height / 20);
	private Vector2 timeSize = new Vector2(Screen.width - Screen.width / 1.5f, Screen.height / 10);// = new Vector2(Screen.width, Screen.height);
	public float timeBarDisplay; //current progress
	
	public int gameTime;
	public int timeSpent;
	private int maxTime;
	public int round;

	public float timeBeforeTransition; // before going to transition after minigame
	
	public string nextGame;
	public string nextHint;	// hint to display in transition between games

	public Vector2 lives; // lives.x = player1, lives.y = player2
	public Vector2 gameIsOver; // format (a,b): a = is game over? (1 or 0), b = who won? (1 or 2)

	public bool timerOverride; // turns off timerbar in GUI
	public bool winnerOverride; // turns off winner text at the end of a minigame

	public bool singleGame;
	public bool overrideTimer; // STOPS timer coroutine

	public bool nuxMode; // when true lives aren't decremented

	// private vars

	private bool transitioning;
	private bool fullGameIsOver;
	private bool quickLoss; // to play the dumb 'win' sound

	private float winUIOffset;

	private string winnerString;

	public string[] minigames;
	public string[] hints;

	private GameObject hiddenNuxController; // reference to the ultimate gamecontroller

	private List<string> uGames;
	private List<string> uHints;

	void Awake () {
	
		if (gc == null)	// make sure this is the only GameController
			gc = this;
		else if (gc != null) {
			Destroy (gc);
			gc = this;
		}

		hiddenNuxController = GameObject.Find ("HiddenNuxController");

		aSource = GetComponent<AudioSource> ();

		GameObject nuxController = GameObject.Find ("HiddenNuxController"); // get nuxmode from hiddenNuxController so there's no confusion
		if (nuxController != null)
			nuxMode = nuxController.GetComponent<HiddenNuxController> ().nuxMode;

		minigames = new string[] {"Button Masher", "PlatformRace", "Racing", "Pong","Fruitman", "FruitmanBlocks", "Enemy Crab Shooter", "FallingBlocks", "Bowling", "VerticalRace", "Maze", "Snowball", "Climber", "Quick Draw", "RollABall", "Parachute"}; // add scene names here separated by comma
		hints = new string[] {"MASH ACTION!", "RUN!", "DRIVE!", "PING!", "KILL!", "SOLVE!", "SHOOT!", "DODGE!", "BOWL!", "CLIMB!", "SCREEN PEAK!", "ROLL!", "GOTTA GO FAST!", "PATIENCE..", "EAT!", "LAND!"};	// add corresponding hint for scene here to show in transition

		uGames = new List<string> (minigames);
		uHints = new List<string> (hints);

		DontDestroyOnLoad (this); // persist between scenes
	}

	void FixedUpdate () { // wait for input to go back to main menu

		if (Input.GetKeyDown (KeyCode.R) && fullGameIsOver) { // Go back to main menu if game is fully over 
			Application.LoadLevel ("Main Menu");
			Destroy (this);
		}

		if (Input.GetKeyDown (KeyCode.N))
			nuxMode = !nuxMode;

		winnerText.enabled = !winnerOverride;
	}

	void OnGUI() {	// draw timer & nuxmode notification

		if (nuxMode) // indicator that NuxMode is on
			GUI.Label (new Rect (Screen.width / 90, Screen.height - Screen.height / 20, Screen.width, Screen.height), "NuxMode ENABLED!");

		if (!timerOverride) { // draw the timer bar
			GUI.BeginGroup(new Rect(timePos.x, timePos.y, timeSize.x, timeSize.y));
			GUI.DrawTexture(new Rect(0,0, timeSize.x, timeSize.y), timeEmptyTex);
			
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, timeSize.x * timeBarDisplay, timeSize.y));
			GUI.DrawTexture(new Rect(0,0, timeSize.x, timeSize.y), timeFullTex);
			GUI.EndGroup();
			GUI.EndGroup();
		}
	}

	public void transition () {	// call this to start transition to next game

		transitioning = true; // tell timer we're transitioning
		if (!singleGame) {
			if (uGames.Count < 1) { // if there are no more minigames left in the cycle
				uGames = new List<string> (minigames);
				uHints = new List<string> (hints); // start a new cycle
			}

			int idx = Random.Range (0, uGames.Count); // generate new game from unplayed games list
			nextGame = uGames [idx]; // set game & hint
			nextHint = uHints [idx];

			if (nextGame == "Climber") {
				if (Random.Range (0,2) == 0)
					nextGame = "Fast Runner";
			}

			uGames.RemoveAt (idx); // remove that game & hint from respective lists
			uHints.RemoveAt (idx);
		}
		StartCoroutine (startTimer ()); // start the game timer
	}

	public void startGame (int time) {	// call this to start the game with time parameter

		gameTime = time * 10; // start with given time
		nextGame = "Transition"; // after this game, do a transition
		round++; // increment round
		rounds.text = "Round " + round;
		StartCoroutine (startTimer ()); // start the timer
	}

	void resetGameController () { // called to reset all values
		round = 0;
		lives = new Vector2 (0, 0);
		fullGameIsOver = false;
		gameIsOver = new Vector2 (0, 0);
		nextGame = "Transition";
		transitioning = false;
		timerOverride = false;
		winnerOverride = false;
	}

	public void gameOver(int winner) { // pass the winner in here at the end of each minigame (0 = draw)

		if (maxTime - gameTime < 10)
			quickLoss = true;

		if (winner == 1) { // subtract lives
			if (!nuxMode)
				lives.y--;
			if (lives.y <= 0) {
				fullGameIsOver = true; // also check to make sure it isn't a game over for either player
				lives.y = 0;
			}
			livesNumP2.text = "" + lives.y;
		} else if (winner == 2) {
			if (!nuxMode) 
				lives.x--;
			if (lives.x <= 0) {
				fullGameIsOver = true;
				lives.x = 0;
			}
			livesNumP1.text = "" + lives.x;
		}

		gameIsOver = new Vector2 (1, winner); // declare winner

		if (gameIsOver.x == 1) { // text update for winners
			
			if (!fullGameIsOver) { // and it's not the end of the game
				if (gameIsOver.y != 0) {
					if (gameIsOver.y == 1)
						winnerText.color = Color.red;
					else
						winnerText.color = Color.blue;
					winnerString = "Player " + (int)gameIsOver.y + " WON!";
				} else {
					winnerText.color = Color.white;
					winnerString = "DRAW!";
				}
				
				winnerText.text = winnerString; // set gui text
			}
		}
		
		if (fullGameIsOver)
			Application.LoadLevel ("Game Over");
	}

	public IEnumerator startTimer () {	// timer. Get ref to this controller & call this coroutine to start

		timeBarDisplay = 1;

		if (!fullGameIsOver) {

			maxTime = gameTime;
			for (int i = maxTime; i >= 0; i--) { // count down & update time on GUI
				yield return new WaitForSeconds (.1f);

				if (!overrideTimer)
					timeSpent = maxTime - gameTime;
				if (timeSpent < 10)
					quickLoss = true;

				if (overrideTimer) 
					break; // break out of the timer so that you can stop if someone wins early

				if (maxTime - gameTime >= 10)
					quickLoss = false;
			

				gameTime = i;
				timeBarDisplay = ((float)gameTime) / ((float)maxTime);
			}

			if (transitioning) {					// if there was a transition there's no need to wait to go to next scene
				transitioning = false;
				if (!fullGameIsOver)
					Application.LoadLevel (nextGame);
			} 
			else {
				yield return new WaitForSeconds (0.1f);
				if (hiddenNuxController != null && hiddenNuxController.GetComponent<HiddenNuxController> ().playSuperFanFare)
					aSource.clip = superFanFare;
				
				else if (gameIsOver.y != 0) { // if its not a draw and hiddenNuxController isn't gonna play the sound
					if (quickLoss) // game ended in the first second
						aSource.clip = quickFailure;
					else if (Application.loadedLevelName == "Pong" || Application.loadedLevelName == "Climber" || Application.loadedLevelName == "Fast Runner" || Application.loadedLevelName == "Maze" || Application.loadedLevelName == "Racing" || Application.loadedLevelName == "Enemy Crab Shooter")
						aSource.clip = retroWin;
					else if (Application.loadedLevelName == "Quick Draw")
						aSource.clip = gunShot;
					else
						aSource.clip = somberFanFare;
				}
				else
					aSource.clip = booSound; // there was a draw, boo 'em
				
				if (aSource.clip != null)
					aSource.Play ();
				
				if (hiddenNuxController != null)
					hiddenNuxController.GetComponent<HiddenNuxController>().playSuperFanFare = false;
				quickLoss = false; // reset after playing the sound

				yield return new WaitForSeconds (timeBeforeTransition - 0.1f);	// after timer is over(this) & winner is displayed (game-side), start next scene

					timeBeforeTransition = 2;
				if (singleGame) {
					Application.LoadLevel ("Game Select");
					Destroy(gameObject);
				}
				winnerText.text = ""; // reset gui text;
				if (!fullGameIsOver && !singleGame) {
					Application.LoadLevel (nextGame);
					overrideTimer = false;
					timerOverride = false; // Reset gui overrides
					gameIsOver = new Vector2 (0, 0); // reset gameIsOver for next game
				}
			}
		}
	}
}