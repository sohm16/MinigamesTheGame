using UnityEngine;
using System.Collections;


public class carController : MonoBehaviour {
	private GameController gc;
	private float translation;
	private float rotation;

	private AudioSource aSource;
	
	public float speed = 15.0F;
	public float rotationSpeed = 250.0F;

	void Start () {

		aSource = GetComponent<AudioSource> ();
		gc = GameController.gc;
	}
	
	void FixedUpdate() {
		if (gameObject.name == "Player1" && gc.gameTime > 0) {

			if (Input.GetButton("Player1Button"))
				translation = -speed;
			else if (Input.GetButton("RacingDown1"))
				translation = speed;
			else
				translation = Input.GetAxis ("VerticalRacing1") * speed * -1;

			rotation = Input.GetAxis ("Player1Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate (0, 0, translation);
			if (translation != 0)
				transform.Rotate (0, rotation, 0);

		}
		if (gameObject.name == "Player2" && gc.gameTime > 0) {

			if (Input.GetButton("Player2Button") || Input.GetKey(KeyCode.Alpha0))
				translation = -speed;
			else if (Input.GetButton("RacingDown2"))
				translation = speed;
			else
				translation = Input.GetAxis ("VerticalRacing2") * speed * -1;
			
			rotation = Input.GetAxis ("Player2Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate (0, 0, translation);
			if (translation != 0)
				transform.Rotate (0, rotation, 0);
		}

	}

	void OnTriggerEnter(Collider other) {

		aSource.Play ();

		if (other.tag == "pickUp") {
			if (gameObject.name == "Player1") {
				Destroy(other.gameObject);
				RacingGameController.instance.score1++;
			}
			else if (gameObject.name == "Player2") {
				Destroy(other.gameObject);
				RacingGameController.instance.score2++;
			}

			RacingGameController.instance.updateText();
		}
	}
	
}

