using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyController : MonoBehaviour {

	public Image readyP1;
	public Image readyP2;
	public Text countDown;
	
	private bool p1Ready;
	private bool p2Ready;
	private int countingDown;

	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKeyDown (KeyCode.T) || Input.GetKeyDown ("joystick 1 button 0")) {

			p1Ready = !p1Ready; // enable / disable ready splash
			readyP1.enabled = p1Ready; // show the ready splash
		}

		if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (KeyCode.Alpha0) || Input.GetKeyDown ("joystick 2 button 0")) {

			p2Ready = !p2Ready;
			readyP2.enabled = p2Ready; 
		}

		if (p1Ready && p2Ready)
			countDown.enabled = true;

		if (countingDown == 0 && p1Ready && p2Ready) { // dont start multiple timers
			countingDown++;
			StartCoroutine (startCountDown ()); // both players are ready, start countdown
		} 

		else if (!p1Ready || !p2Ready) {
			countDown.enabled = false; // someone unreadied, stoppit
		}
	}

	private IEnumerator startCountDown() { // start 3 sec countdown to game start (while p1 & p2 are ready)

		for (int i = 35; i >= 5; i--) {

			if (countingDown == 0 || !p1Ready || !p2Ready)  // if they change their minds, break out
				break;

			if (countingDown == 1 && p1Ready && p2Ready)
				countDown.text = "" + i / 10; // update text

			if (p1Ready && p2Ready && i <= 5) // timer's up & we're still ready, load the game
				Application.LoadLevel ("Transition");

			yield return new WaitForSeconds (.1f);
		}
		countingDown--;
	}
}
