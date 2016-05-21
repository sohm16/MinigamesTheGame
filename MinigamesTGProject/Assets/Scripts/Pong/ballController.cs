using UnityEngine;
using System.Collections;

public class ballController : MonoBehaviour {

	public int time;

	private Rigidbody rb;
	private Vector3 initialImpulse;
	private GameController gc;
	private bool gameOver;
	public float initForce;

	private AudioSource aSource;

	public bool draw; // for hiddenNux

	// Use this for initialization
	void Start () {

		aSource = GetComponent<AudioSource> ();

		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 2;
		rb = this.GetComponent<Rigidbody> ();

		Vector3 verticalImpulse = new Vector3 (0, Random.Range(-3f, 3f), 0);
		Vector3 horizontalImpulse = new Vector3 (Random.Range (-1f, 1f), 0, 0);
		horizontalImpulse.Normalize ();
		horizontalImpulse *= initForce;
		rb.AddForce (verticalImpulse, ForceMode.Impulse);
		rb.AddForce (horizontalImpulse, ForceMode.Impulse);

	}

	void FixedUpdate() {
		//rb.velocity = new Vector3 (rb.velocity.x * (speed * Time.deltaTime), rb.velocity.y * (speed * Time.deltaTime), 0);
		//transform.Translate(transform.position * 1.1f * Time.deltaTime);
		//rb.MovePosition (rb.position + rb.position * 2f * Time.deltaTime);
		//force += transform.forward * speed * Time.deltaTime;
		//cf.force += transform.position * 2 * Time.deltaTime;
		//rb.AddForce (rb.velocity *3*Time.deltaTime, ForceMode.Acceleration);

		if (gc.gameTime <= 0 && !gameOver) {
			gc.gameOver (0);
			rb.constraints = RigidbodyConstraints.FreezeAll;
			draw = true;
		}

	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "player1Wall") {
			gameOver = true;
			gc.gameOver (2);
			gc.gameTime = 0;
			gc.overrideTimer = true;
		} 
		else if (other.tag == "player2Wall") {
			gameOver = true;
			gc.gameOver (1);
			gc.gameTime = 0;
			gc.overrideTimer = true;
		}
	}

	
	void OnCollisionEnter(Collision other) { // if the player collides with a ball, play the sound

		aSource.Play ();
	}


}
