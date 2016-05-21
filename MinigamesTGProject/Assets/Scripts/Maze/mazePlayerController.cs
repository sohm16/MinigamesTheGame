using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mazePlayerController : MonoBehaviour {
	public float speed;

	private int score1;
	private int score2;
	private CharacterController controller;
	
	void Start () {

		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (this.tag == "Player1") {
			Vector3 movement = new Vector3 (Input.GetAxis ("Player1Horizontal"), 0, Input.GetAxis ("Player1Vertical"));
			controller.SimpleMove (movement * speed);
		}
		if (this.tag == "Player2") {
			Vector3 movement = new Vector3 (Input.GetAxis ("Player2Horizontal"), 0, Input.GetAxis ("Player2Vertical"));
			controller.SimpleMove (movement * speed);
		}
	}


	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.tag == "items") {
			Destroy(hit.transform.parent.gameObject);

			if (controller.gameObject.tag == "Player1")
				mazeGameManager.instance.score1++;
			if (controller.gameObject.tag == "Player2")		
				mazeGameManager.instance.score2++;

			mazeGameManager.instance.updateScoreText();


		}
	}

}
