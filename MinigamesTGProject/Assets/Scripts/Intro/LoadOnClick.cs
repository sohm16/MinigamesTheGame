using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	private GameController gc;
	public GameObject controllerSpawner;

	private int idx;

	public void LoadScene(string level)
	{
		if (Application.loadedLevelName == "Game Select" && level != "Main Menu" && level != "THANKYOU") { // if we're on the game select screen

				Instantiate (controllerSpawner, transform.position, transform.rotation); // spawn a gamecontroller
				gc = GameController.gc; // and set select game to true so the game stops at the end
				gc.singleGame = true;

				for (idx = 0; idx < gc.minigames.Length; idx++) { // find the idx of minigame we're loading
					if (level == gc.minigames[idx])
						break;
				}
				gc.nextHint = gc.hints[idx]; // get a hint out of gamemanager for the transition

				if (level == "Climber" && Random.Range (0,2) == 0) // just in case its fast runner..
					level = "Fast Runner";
				gc.nextGame = level;
				Application.LoadLevel ("Transition"); // start a transition
			
		}

		else
			Application.LoadLevel (level); // not on game select screen, just start whatever it says
	}

}
