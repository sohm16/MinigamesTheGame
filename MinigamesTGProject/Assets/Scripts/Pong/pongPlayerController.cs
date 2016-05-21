using UnityEngine;
using System.Collections;

public class pongPlayerController : MonoBehaviour {
	public float speed;
	private GameController gc;

	// Use this for initialization
	void Start () {
		speed = 1;
		gc = GameController.gc;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (gameObject.name == "Player1" && gc.gameTime > 0) {
			float translation = Input.GetAxis ("VerticalPong1") * speed;
			transform.Translate (new Vector3 (0, translation, 0));
			checkBounds();
		}
		else if (gameObject.name == "Player2" && gc.gameTime > 0) {
			float translation = Input.GetAxis ("VerticalPong2") * speed;
			transform.Translate (new Vector3 (0, translation, 0));
			checkBounds();
		}


	}

	void checkBounds(){
		// check bounds
		if (transform.position.y > 14) {
			Vector3 temp = transform.position; 
			temp.y = 14;
			transform.position = temp; 
			
		}
		if (transform.position.y < -12) {
			Vector3 temp = transform.position; 
			temp.y = -12;
			transform.position = temp; 
			
		}
	}
}
