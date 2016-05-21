using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionController : MonoBehaviour {

	private GameController gc;

	public Text hintText;

	public int timeToGame; // from transition to game time

	// Use this for initialization
	void Start () {
	
		gc = GameController.gc;
		gc.gameTime = timeToGame * 10;
		gc.transition ();

		hintText.text = gc.nextHint;
	}

	private IEnumerator animateText() {

		for (int i = -Screen.width / 3; i < Screen.width + Screen.width; i+=10) { // zoom towards and then out
			hintText.transform.position = new Vector2 (i, hintText.transform.position.y);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
