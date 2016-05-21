using UnityEngine;
using System.Collections;

public class JumpChecker : MonoBehaviour {
	
	public bool p1;
	public bool gameOver;

	public float jumpForce;
	public float movementSpeed;

	public GameObject player1;
	public GameObject player2;

	private bool canJump;

	private float translation;

	void Start() {

		Physics.IgnoreCollision (player1.GetComponent<Collider> (), player2.GetComponent<Collider> ()); // ignore other player
	}

	void FixedUpdate() {

//		Debug.Log (numFloatChecks);

		if (p1 && !gameOver) { // if its player one we're dealing with
				translation = Input.GetAxis("HorizontalPlatformer1") * Time.deltaTime * movementSpeed;
				transform.Translate (translation, 0, 0);

			if ((Input.GetKey (KeyCode.W) || Input.GetKey ("joystick 1 button 0") || Input.GetKey (KeyCode.T)) && canJump)
				GetComponentInParent<Rigidbody> ().AddForce (0, jumpForce, 0); // jump
		} 

		else if (!gameOver) { // we're dealing with player 2
			translation = Input.GetAxis("HorizontalPlatformer2") * Time.deltaTime * movementSpeed;
			transform.Translate (translation, 0, 0);
			
			if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey ("joystick 2 button 0") || Input.GetKey (KeyCode.Alpha0) || Input.GetKey(KeyCode.Keypad0)) && canJump)
				GetComponentInParent<Rigidbody> ().AddForce (0, jumpForce, 0); // jump
		}
	}



	void OnTriggerEnter(Collider other) { // handle animations and the ability to jump

		if (other.gameObject.tag == "Floor") {

			canJump = true;
			GetComponentInParent<Animator> ().ResetTrigger ("Jump");
			GetComponentInParent<Animator>().SetTrigger ("Idle");
		}
	}

	void OnTriggerExit(Collider other) { // player's jumped

		if (other.gameObject.tag == "Floor") { // player leaves the floor

			canJump = false;
			GetComponentInParent<Animator> ().ResetTrigger ("Idle");
			GetComponentInParent<Animator> ().SetTrigger ("Jump");
		}
	}
}
