using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mazeGameManager : MonoBehaviour {
	public static mazeGameManager instance = null;
	public Text score1Text;
	public Text score2Text;
	public int score1;
	public int score2;
	public int time;
	public GameObject all1;
	public GameObject all2;
	public GameObject player1Prefab; // light
	public GameObject player2Prefab; // dark
	public GameObject darkP1; // dark
	public GameObject darkP2; // light
	public Material player1Material;
	public Material player2Material;

	private Camera cam1;
	private Camera cam2;
	private bool onlyOnce;
	private bool switched;
	private GameController gc;

	//private GameObject player1;
	//private GameObject player2;


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    


		Instantiate (all1, new Vector3 (-10f, 0, 0), Quaternion.identity);
		Instantiate (all2, new Vector3 (10f, 0, 0), Quaternion.identity);
		//Instantiate (all2, new Vector3 (10f, 0, 0), Quaternion.identity);

		cam1 = GameObject.FindWithTag ("cam1").GetComponent<Camera> ();
		cam2 = GameObject.FindWithTag ("cam2").GetComponent<Camera> ();

		if (Random.Range (0, 2) == 0) {
			//Instantiate (player1Prefab, new Vector3 (-16f, 0.3f, -6f), Quaternion.identity);
			//Instantiate (player2Prefab, new Vector3 (16f, 0.3f, -6f), Quaternion.identity);
			Instantiate (player1Prefab, new Vector3 (-16f, 0.3f, -6f), Quaternion.identity);
			Instantiate (player2Prefab, new Vector3 (16f, 0.3f, -6f), Quaternion.identity);
		} 
		else {
			//cam1.rect = new Rect (0.5f, 0f, 0.5f, 1f);
			//cam2.rect = new Rect (0f, 0f, 0.5f, 1f);
			//Instantiate (darkP2, new Vector3(-4f, 0.3f, -6f), Quaternion.identity);
//			Instantiate (player1Prefab, new Vector3(4f, 0.3f, -6f), Quaternion.identity);
//			GameObject.FindWithTag("Player1").tag = "Player2";
//			GameObject.FindWithTag("Player2").tag = "Player1";
			cam1.rect = new Rect (0.5f, 0f, 0.5f, 1f);
			cam2.rect = new Rect (0f, 0f, 0.5f, 1f);
			Instantiate (player1Prefab, new Vector3(-4f, 0.3f, -6f), Quaternion.identity);
			Instantiate (player2Prefab, new Vector3(4f, 0.3f, -6f), Quaternion.identity);
			GameObject.FindWithTag("Player1").tag = "Player2";
			GameObject.FindWithTag("Player2").tag = "Player1";
			//Material temp;
			//GameObject.FindWithTag("Player2").GetComponent<Renderer>().material = temp;
			GameObject.FindWithTag("Player1").GetComponent<Renderer>().material = player1Material;
			GameObject.FindWithTag("Player2").GetComponent<Renderer>().material = player2Material;

		}


		for (int i = 0; i < 3;) {
			
			GameObject itemPair = GameObject.FindWithTag ("pairs").transform.GetChild (Random.Range (0, 8)).gameObject;
			if (!itemPair.activeInHierarchy) {
				itemPair.SetActive (true);
				i++;
			}
		}
	}

	void Start () {
		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 2;

//		if (switched) {
//			GameObject.Find("Player1").tag = "Player2";
//			GameObject.Find("Player2").tag = "Player1";
//		}

	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectWithTag ("items") == null) { // stop the game when all items are picked up

			gc.overrideTimer = true;
			gc.gameTime = 0;
		}

		if (gc.gameTime <= 0 && !onlyOnce) {
			onlyOnce = true;
			if (score1 > score2)
				gc.gameOver (1);
			else if (score1 < score2) 
				gc.gameOver (2);
			else if (score1 == score2)
				gc.gameOver (0);
		}

	}

	public void updateScoreText (){
		score1Text.text = "Score: " + score1;
		score2Text.text = "Score: " + score2;
	}
}
