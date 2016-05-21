using UnityEngine;
using System.Collections;

public class FruitmanController : MonoBehaviour {

	public GameObject bananarang;

	public float movementSpeed;
	public float shotSpeed;
	public float shotDelay;

	public bool p1;

	public AudioClip enemyDie;
	public AudioClip banana;

	private bool canShoot;

	private string direction;
	private string lastTrigger;

	private AudioSource aSource;

	private Animator anim;

	private int numEnemies;

	private float translationX;
	private float translationY;

	private GameController gc;

	// Use this for initialization
	void Start() {

		canShoot = true;
		aSource = GetComponent<AudioSource> ();
		gc = GameController.gc;
		anim = GetComponent<Animator> ();
		lastTrigger = "Warning"; // to avoid anny warnings
		anim.SetTrigger ("Warning");
		direction = "Down";
		animate ("Down");

		if (p1)
			numEnemies = GameObject.FindGameObjectsWithTag ("Enemies1").Length;
		else			
			numEnemies = GameObject.FindGameObjectsWithTag ("Enemies2").Length;
	}
	
	void FixedUpdate() {

		if (p1 && gc.gameTime > 0) { // if its player one we're dealing with while time is not up

			if (GameObject.FindGameObjectsWithTag ("Enemies1").Length < numEnemies) { // killed enemy, make sound
				aSource.clip = enemyDie;
				aSource.Play();
				numEnemies = GameObject.FindGameObjectsWithTag("Enemies1").Length;
			}

			if (canShoot && (Input.GetKey (KeyCode.Y) || Input.GetKey (KeyCode.T) || Input.GetKeyDown ("joystick 1 button 0")))
			    StartCoroutine (fire ());

			translationX = 0f;
			translationY = 0f;

			translationX = Input.GetAxisRaw ("FruitHorizontal1");
			translationY = Input.GetAxisRaw ("FruitVertical1");

			if (translationX == 1) animate("Right");
			if (translationX == -1) animate ("Left");
			if (translationY == 1) animate ("Up");
			if (translationY == -1) animate ("Down");

			transform.Translate (new Vector3(translationX, translationY, 0) * movementSpeed * Time.deltaTime);
		} 
		
		if (!p1 && gc.gameTime > 0) { // we're dealing with player 2

			if (GameObject.FindGameObjectsWithTag ("Enemies2").Length < numEnemies) { // kill enemy sound
				aSource.clip = enemyDie;
				aSource.Play();
				numEnemies = GameObject.FindGameObjectsWithTag("Enemies2").Length;
			}

			if (canShoot && (Input.GetKey (KeyCode.KeypadPeriod) || Input.GetKey (KeyCode.Keypad0) || Input.GetKey (KeyCode.Alpha0) || Input.GetKey (KeyCode.Period) || Input.GetKeyDown ("joystick 2 button 0")))
				StartCoroutine (fire ());

			translationX = 0f;
			translationY = 0f;
			
			translationX = Input.GetAxisRaw ("FruitHorizontal2");
			translationY = Input.GetAxisRaw ("FruitVertical2");
			
			if (translationX == 1) animate("Right");
			if (translationX == -1) animate ("Left");
			if (translationY == 1) animate ("Up");
			if (translationY == -1) animate ("Down");
			
			transform.Translate (new Vector3(translationX, translationY, 0) * movementSpeed * Time.deltaTime);
		}
	}

	private IEnumerator fire() {

		GameObject projectile = null;
		canShoot = false;

		if (gc.gameTime > 0) { // while the game is going, you can shoot
			projectile = (GameObject)Instantiate (bananarang, transform.position, transform.rotation);

			if (p1)
				projectile.GetComponent<SpriteRenderer>().color = Color.green; // color shot accordingly
			else
				projectile.GetComponent<SpriteRenderer>().color = Color.red;

			if (direction == "Up")
				projectile.GetComponent<Rigidbody> ().velocity = new Vector2 (0, shotSpeed); // set direction
			else if (direction == "Down")
				projectile.GetComponent<Rigidbody> ().velocity = new Vector2 (0, -shotSpeed);
			else if (direction == "Left")
				projectile.GetComponent<Rigidbody> ().velocity = new Vector2 (-shotSpeed, 0);
			else
				projectile.GetComponent<Rigidbody> ().velocity = new Vector2 (shotSpeed, 0);

			aSource.clip = banana;
			aSource.Play ();
			yield return new WaitForSeconds (shotDelay); // then wait for next loop
			canShoot = true;
		}
	}

	void animate(string facing) { // make the animations all nice and pretty

		direction = facing;
		anim.ResetTrigger (lastTrigger);
		anim.SetTrigger (facing);
		lastTrigger = facing;
	}

	void OnTriggerEnter(Collider other) { // for fruit man block win decision

		if (other.gameObject.name == "Plate") {
			gc.overrideTimer = true;
			if (p1)
				gc.gameOver (1);
			else
				gc.gameOver (2);
			gc.gameTime = 0;
		}
	}
}
