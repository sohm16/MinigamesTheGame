using UnityEngine;
using System.Collections;

public class CrabShooterController : MonoBehaviour {

	private GameController gc;

	public int time;
	public int playerSpeed;

	public GameObject player1;
	public GameObject player2;
	public GameObject player1Shot;
	public GameObject player2Shot;

	public float reloadTime;

	public int spentTime; // for hiddenNux

	public Boundary boundary;

	private AudioSource aSource;
	private bool onlyOnce;
	private float player1X;
	private float player1Y;
	private float player2X;
	private float player2Y;
	private float player1Reload;
	private float player2Reload;

	// Use this for initialization
	void Start () {
		gc = GameController.gc; // get GameController
		aSource = GetComponent<AudioSource> ();
		gc.startGame (time); // start game with 5 seconds on the clock
		player1Reload = Time.time;
		player2Reload = Time.time;

		StartCoroutine (timeSpent ()); // for hiddenNux
	}
	
	// Update is called once per frame
	void Update () {
		player1X = 0f;
		player1Y = 0f;
		player2X = 0f;
		player2Y = 0f;
	
		if (gc.gameTime > 0) {

			if ((Input.GetKey(KeyCode.T) || Input.GetKey ("joystick 1 button 1") ) && (Time.time > player1Reload)) {
				player1Reload = Time.time + reloadTime;
				Instantiate (player1Shot, new Vector3(player1.transform.position.x, player1.transform.position.y, 0f), Quaternion.identity );
				aSource.Play ();
			}

			if ( (Input.GetKey(KeyCode.Keypad0) || Input.GetKey ("joystick 2 button 1") || Input.GetKey (KeyCode.Alpha0) ) && Time.time > player2Reload) {
				player2Reload = Time.time + reloadTime;
				Instantiate (player2Shot, new Vector3(player2.transform.position.x, player2.transform.position.y, 0f), Quaternion.identity );
				aSource.Play ();
			}

			player1X = Input.GetAxis ("Player1Horizontal");
			player1Y = Input.GetAxis ("Player1Vertical");

			player2X = Input.GetAxis ("Player2Horizontal");
			player2Y = Input.GetAxis ("Player2Vertical");

			player1.transform.Translate ( new Vector3 (player1X, player1Y, 0f) * playerSpeed * Time.deltaTime );
			player2.transform.Translate ( new Vector3 (player2X, player2Y, 0f) * playerSpeed * Time.deltaTime );

			player1.transform.position = new Vector3 (
				Mathf.Clamp (player1.transform.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp (player1.transform.position.y, boundary.yMin, boundary.yMax),
				0f);

			player2.transform.position = new Vector3 (
				Mathf.Clamp (player2.transform.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp (player2.transform.position.y, boundary.yMin, boundary.yMax),
				0f);
		} 
	}

	private IEnumerator timeSpent() { // for hiddenNux

		while (gc.gameIsOver.x != 1) {
			yield return new WaitForSeconds (0.1f);
			spentTime++;
		}
	}
}

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}
