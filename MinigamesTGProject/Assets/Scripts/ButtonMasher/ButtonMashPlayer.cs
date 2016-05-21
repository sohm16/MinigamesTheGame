using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonMashPlayer : MonoBehaviour {

	private GameController gc;	

	public int myScore;
	public bool p1;

	// Use this for initialization
	void Start () {
	
		gc = GameController.gc;
		myScore = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (p1 && gc.gameTime > 0) {
			if (Input.GetKeyDown (KeyCode.T) || Input.GetKeyDown ("joystick 1 button 0")) {
	
				myScore++;
				GetComponent<Text> ().text = "Player 1\nScore\n\n" + myScore;
			}
		} 

		else if (gc.gameTime > 0) {
			if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown ("joystick 2 button 0") || Input.GetKeyDown (KeyCode.Alpha0)) {
				myScore++;
				GetComponent<Text> ().text = "Player 2\nScore\n\n" + myScore;
			}
		}
	}
}
