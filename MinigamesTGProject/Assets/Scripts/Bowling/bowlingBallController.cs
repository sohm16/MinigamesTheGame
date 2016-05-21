using UnityEngine;
using System.Collections;

public class bowlingBallController : MonoBehaviour {
	private ConstantForce cf;
	private GameController gc;

	public GameObject cam1;
	public GameObject cam2;
	public bool ballLaunched;
	public float speed;
	// Use this for initialization
	void Start () {
		gc = GameController.gc;
		cf = GetComponent<ConstantForce> ();
		speed *= Time.deltaTime;
		ballLaunched = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Bowling") { // for credits
			if (gc.gameTime <= 0)
				speed = 0;

			transform.position += new Vector3 (speed, 0, 0);
			if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("joystick 1 button 0") || Input.GetKeyDown (KeyCode.T)) && this.name == "ball" && gc.gameTime > 0) {
				speed = 0;
				ballLaunched = true;
				cf.force = new Vector3 (0, 0, 20);
				cam1.GetComponent<ConstantForce> ().force = new Vector3 (0, 0, 3);
			}	
			if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown ("joystick 2 button 0") || Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (KeyCode.Alpha0)) && this.name == "ball2" && gc.gameTime > 0) {
				speed = 0;
				ballLaunched = true;
				cf.force = new Vector3 (0, 0, 20);
				cam2.GetComponent<ConstantForce> ().force = new Vector3 (0, 0, 3);
			}	

			cam1.transform.position = new Vector3 (cam1.transform.position.x, cam1.transform.position.y, Mathf.Clamp (cam1.transform.position.z, -7f, -2.5f));
			cam2.transform.position = new Vector3 (cam2.transform.position.x, cam2.transform.position.y, Mathf.Clamp (cam2.transform.position.z, -7f, -2.5f));
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Boundary") 
			speed *= -1;
	}
}
