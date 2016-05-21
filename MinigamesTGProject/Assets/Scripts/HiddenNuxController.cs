using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HiddenNuxController : MonoBehaviour {

	private GameController gc;

	public bool bowlingNux; // hidden nuxes
	public bool racingNux;
	public bool snowNux;
	public bool vertNux;
	public bool platformNux;
	public bool mazeNux;
	public bool climbNux;
	public bool drawNux;
	public bool crabNux;
	public bool pongNux;
	public bool mashNux;
	public bool fallingNux;
	public bool fruitNux;
	public bool solveNux;
	public bool parachuteNux;
	public bool rollABallNux;

	public bool nuxCylinder; // for parachute

	public bool playSuperFanFare; // player filled a hidden nux requirement

	private bool allNuxes;

	public bool fruitKill1; // fruitman kill minigame
	public bool fruitKill2;
	public bool fruitKill3;
	public bool fruitKill4;

	public bool platform1; // platformRace minigame
	public bool platform2;
	public bool platform3;

	public bool fruitSolve1; // fruitman solve minigame
	public bool fruitSolve2;
	public bool fruitSolve3;

	public bool vert1; // verticalRace minigame
	public bool vert2;
	public bool vert3;

	public bool draw1; // quickdraw
	public bool draw2;
	public bool draw3;
	public bool draw4;

	public bool snow1; // snowball minigame
	public bool snow2;
	public bool snow3;

	public bool fast1; // fast games
	public bool fast2;

	public bool nuxMode;

	private GameObject creditsButton;
	private GameObject controller;

	// Use this for initialization
	void Start () {
	
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.N)) { // nuxMode for credits
			gc = GameController.gc;
			if (gc != null)
				nuxMode = gc.nuxMode; // if theres a gamecontroller, just take the value of nuxmode from that
			else
				nuxMode = !nuxMode; // otherwise just toggle it
		}


		if (!mashNux && Application.loadedLevelName == "Button Masher") { // get mashNux if someone gets 30+ score
			controller = GameObject.Find ("ButtonMasherController");
			if (controller.GetComponent<MasherController> ().p1ScoreVal >= 30 || controller.GetComponent<MasherController> ().p2ScoreVal >= 30) 
				win ("Mash");
		} else if (!bowlingNux && Application.loadedLevelName == "Bowling") { // both players get a strike in bowling
			controller = GameObject.Find ("Score Keeper");
			if (controller.GetComponent<bowlingScoreKeeper> ().score1 >= 10 && controller.GetComponent<bowlingScoreKeeper> ().score2 >= 10)
				win("Bowling");
	} else if (!crabNux && Application.loadedLevelName == "Enemy Crab Shooter") { // kill the crab in .5 sec or less
			controller = GameObject.Find ("Crab Shooter Controller");
			if (controller.GetComponent<CrabShooterController> ().spentTime <= 5)
				win ("Crab");
	} else if (!pongNux && Application.loadedLevelName == "Pong") { // get a draw in pong
			controller = GameObject.Find ("Ball");
			if (controller.GetComponent<ballController> ().draw)
				win ("Pong");
		} else if (!racingNux && Application.loadedLevelName == "Racing") { // get all the score balls
			controller = GameObject.Find ("GameManager");
			if (controller.GetComponent<RacingGameController> ().score1 + controller.GetComponent<RacingGameController> ().score2 >= 9)
				win ("Racing");
	} else if (!fallingNux && Application.loadedLevelName == "FallingBlocks") { // get through the whole thing together & draw
			controller = GameObject.Find ("Floor");
			gc = GameController.gc;
			if (controller.GetComponent<FallingBlockController> ().player1.transform.localScale.y > 1 && controller.GetComponent<FallingBlockController> ().player2.transform.localScale.y > 1 && gc != null && gc.gameIsOver.x == 1 && gc.gameIsOver.y == 0)
				win ("Falling");
		} else if (!fruitNux && Application.loadedLevelName == "Fruitman") { // someone destroyed every enemy formation (except #4)

			controller = GameObject.Find ("FruitgameController");
			if (controller.GetComponent<FruitgameController> ().enemiesKilled1 >= 4 || controller.GetComponent<FruitgameController> ().enemiesKilled2 >= 4) {
				if (!fruitKill1 && GameObject.Find ("Enemies1") != null)
					win ("FruitKill1");
				else if (!fruitKill2 && GameObject.Find ("Enemies2") != null)
					win ("FruitKill2");
				else if (!fruitKill3 && GameObject.Find ("Enemies3") != null)
					win ("FruitKill3");
				else if (!fruitKill4 && GameObject.Find ("Enemies4") != null)
					win ("FruitKill4");
			}

			if (fruitKill1 && fruitKill2 && fruitKill3 && fruitKill4)
				win ("FruitKill");
		} else if (!platformNux && Application.loadedLevelName == "PlatformRace") { // get to the end of every level

			controller = GameObject.Find ("PlatformRacerController");
			if (controller != null && controller.GetComponent<PlatformRaceController> ().p1.transform.localPosition.x > 10 || controller.GetComponent<PlatformRaceController> ().p2.transform.localPosition.x > 10) {

				if (!platform1 && GameObject.Find ("Level1") != null)
					win ("Platform1");
				else if (!platform2 && GameObject.Find ("Level2") != null)
					win ("Platform2");
				else if (!platform3 && GameObject.Find ("Level3") != null)
					win ("Platform3");
			}

			if (platform1 && platform2 && platform3)
				win ("Platform");
		} else if (!solveNux && Application.loadedLevelName == "FruitmanBlocks") { // solve every level

			gc = GameController.gc;

			if (!fruitSolve1 && GameObject.Find ("Level1") != null && gc.gameIsOver.y != 0)
				win ("Solve1");
			else if (!fruitSolve2 && GameObject.Find ("Level2") != null && gc.gameIsOver.y != 0)
				win ("Solve2");
			else if (!fruitSolve3 && GameObject.Find ("Level3") != null && gc.gameIsOver.y != 0)
				win ("Solve3");
			
			if (fruitSolve1 && fruitSolve2 && fruitSolve3)
				win ("Solve");
		} else if (!vertNux && Application.loadedLevelName == "VerticalRace") { // play through every level in  vertrace

			controller = GameObject.Find ("PlatformRacerController");

			if (controller.GetComponent<VerticalRaceController> ().p1.transform.localPosition.y > 6.8 || controller.GetComponent<VerticalRaceController> ().p2.transform.localPosition.y > 6.8) {

				if (!vert1 && GameObject.Find ("Level1") != null)
					win ("Vert1");
				else if (!vert2 && GameObject.Find ("Level2") != null)
					win ("Vert2");
				else if (!vert3 && GameObject.Find ("Level3") != null)
					win ("Vert3");
			}

			if (vert1 && vert2 && vert3)
				win ("Vert");
		} else if (!climbNux && Application.loadedLevelName == "Climber") { // somebody get to the top in climber

			controller = GameObject.Find ("Climber Controller");
			if (controller.GetComponent<ClimberController> ().player1.transform.localPosition.y > 85 || controller.GetComponent<ClimberController> ().player2.transform.localPosition.y > 85)
				win ("Fast1");

			if (fast1 && fast2)
				win ("Climb");
		} else if (!climbNux && Application.loadedLevelName == "Fast Runner") {

			controller = GameObject.Find ("FastRunnerController");
			if (controller.GetComponent<FastRunnerController> ().player1.transform.localPosition.x > 184 || controller.GetComponent<FastRunnerController> ().player2.transform.localPosition.x > 184)
				win ("Fast2");

			if (fast1 && fast2)
				win("Climb");
		} else if (!drawNux && Application.loadedLevelName == "Quick Draw") { // somebody get to the top in climber
			
			controller = GameObject.Find ("QuickDrawController");

			if (!draw1)
				draw1 = controller.GetComponent<QuickDrawController> ().draw1;
			if (!draw2)
				draw2 = controller.GetComponent<QuickDrawController> ().draw2;
			if (!draw3)
				draw3 = controller.GetComponent<QuickDrawController> ().draw3;
			if (!draw4)
				draw4 = controller.GetComponent<QuickDrawController> ().draw4;

			if (draw1 && draw2 && draw3 && draw4)
				win ("Draw");
		} else if (!snowNux && Application.loadedLevelName == "Snowball") { // get to the end of each track

			controller = GameObject.Find ("Finishline");
			if (controller != null && controller.GetComponent<SnowballGameController> ().collided) {

				if (!snow1 && GameObject.Find ("Level1") != null)
					win("Snow1");
				else if (!snow2 && GameObject.Find ("Level2") != null)
					win("Snow2");
				else if (!snow3 && GameObject.Find ("Level3") != null)
					win("Snow3");
			}

			if (snow1 && snow2 && snow3)
				win("Snow");
		} else if (!mazeNux && Application.loadedLevelName == "Maze") { // get all the pickups quickly
		
			gc = GameController.gc;
			if (gc.gameIsOver.x == 1 && gc.timeSpent <= 50)
				win("Maze");
		} else if (!parachuteNux && Application.loadedLevelName == "Parachute") { // both players land in the middle of their targets

			controller = GameObject.Find ("Char");
			if (controller.GetComponent<parachutePlayerController>().nuxCylinder || GameObject.Find ("Char2").GetComponent<parachutePlayer2Controller>().nuxCylinder)
				win ("Parachute");

		} else if (!rollABallNux && Application.loadedLevelName == "RollABall") { // find the golden potato in rollaball

			controller = GameObject.Find ("RollABallController");
			if (controller.GetComponent<RollABallController> ().p1.GetComponent<BallController> ().potatoPickedUp || controller.GetComponent<RollABallController> ().p2.GetComponent<BallController> ().potatoPickedUp)
				win ("Eat");
		} else if (Application.loadedLevelName == "Game Select") { // on gameselect, show won nuxes

			if (bowlingNux && pongNux && racingNux && fallingNux && mashNux && mazeNux && climbNux && rollABallNux &&
				vertNux && crabNux && drawNux && snowNux && solveNux && platformNux && fruitNux && parachuteNux)
				allNuxes = true;
				
			if (allNuxes || nuxMode) {
				creditsButton = GameObject.Find ("CreditsButton");
				creditsButton.GetComponent<Image> ().enabled = true;
				creditsButton.GetComponentInChildren<Text> ().enabled = true;
			}
			 else if (!allNuxes && !nuxMode) { // able to turn these off for nuxmode
				creditsButton = GameObject.Find ("CreditsButton");
				creditsButton.GetComponent<Image> ().enabled = false;
				creditsButton.GetComponentInChildren<Text> ().enabled = false;
			}
				
			if (bowlingNux)
				GameObject.Find ("BowlNux").GetComponent<Image> ().enabled = true; // show off which nuxes have been achieved
			if (pongNux)
				GameObject.Find ("PongNux").GetComponent<Image> ().enabled = true;
			if (racingNux)
				GameObject.Find ("RacingNux").GetComponent<Image> ().enabled = true;
			if (fallingNux)
				GameObject.Find ("FallingNux").GetComponent<Image> ().enabled = true;
			if (mashNux)
				GameObject.Find ("MashNux").GetComponent<Image> ().enabled = true;
			if (mazeNux)
				GameObject.Find ("MazeNux").GetComponent<Image> ().enabled = true;
			if (climbNux)
				GameObject.Find ("ClimbNux").GetComponent<Image> ().enabled = true;
			if (rollABallNux)
				GameObject.Find ("RollABallNux").GetComponent<Image> ().enabled = true;
			if (vertNux)
				GameObject.Find ("VertRaceNux").GetComponent<Image> ().enabled = true;
			if (crabNux)
				GameObject.Find ("CrabNux").GetComponent<Image> ().enabled = true;
			if (drawNux)
				GameObject.Find ("DrawNux").GetComponent<Image> ().enabled = true;
			if (snowNux)
				GameObject.Find ("SnowNux").GetComponent<Image> ().enabled = true;
			if (solveNux)
				GameObject.Find ("SolveNux").GetComponent<Image> ().enabled = true;
			if (platformNux)
				GameObject.Find ("PlatformNux").GetComponent<Image> ().enabled = true;
			if (fruitNux)
				GameObject.Find ("KillNux").GetComponent<Image> ().enabled = true;
			if (parachuteNux)
				GameObject.Find ("ParachuteNux").GetComponent<Image> ().enabled = true;
		} 

		else if (Application.loadedLevelName == "THANKYOU") { // go back if u hit esc on credits
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.LoadLevel("Game Select");
		}
	}

	void win(string game) { // easy one liner for 2 bools

		playSuperFanFare = true;
		if (game == "Bowling")
			bowlingNux = true;
		else if (game == "Parachute")
			parachuteNux = true;
		else if (game == "FruitKill")
			fruitNux = true;
		else if (game == "FruitKill1")
			fruitKill1 = true;
		else if (game == "FruitKill2")
			fruitKill2 = true;
		else if (game == "FruitKill3")
			fruitKill3 = true;
		else if (game == "FruitKill4")
			fruitKill4 = true;
		else if (game == "Solve")
			solveNux = true;
		else if (game == "Solve1")
			fruitSolve1 = true;
		else if (game == "Solve2")
			fruitSolve2 = true;
		else if (game == "Solve3")
			fruitSolve3 = true;
		else if (game == "Maze")
			mazeNux = true;
		else if (game == "Platform")
			platformNux = true;
		else if (game == "Platform1")
			platform1 = true;
		else if (game == "Platform2")
			platform2 = true;
		else if (game == "Platform3")
			platform3 = true;
		else if (game == "Climb")
			climbNux = true;
		else if (game == "Fast1")
			fast1 = true;
		else if (game == "Fast2")
			fast2 = true;
		else if (game == "Eat")
			rollABallNux = true;
		else if (game == "Vert")
			vertNux = true;
		else if (game == "Vert1")
			vert1 = true;
		else if (game == "Vert2")
			vert2 = true;
		else if (game == "Vert3")
			vert3 = true;
		else if (game == "Pong")
			pongNux = true;
		else if (game == "Mash")
			mashNux = true;
		else if (game == "Snow")
			snowNux = true;
		else if (game == "Snow1")
			snow1 = true;
		else if (game == "Snow2")
			snow2 = true;
		else if (game == "Snow3")
			snow3 = true;
		else if (game == "Racing")
			racingNux = true;
		else if (game == "Draw")
			drawNux = true;
		else if (game == "Crab")
			crabNux = true;
		else if (game == "Falling")
			fallingNux = true;
	}

	void OnGUI() {

		if (nuxMode) // indicator that NuxMode is on
			GUI.Label (new Rect (Screen.width / 90, Screen.height - Screen.height / 20, Screen.width, Screen.height), "NuxMode ENABLED!");
	}
}
