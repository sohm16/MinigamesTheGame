using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject gameController;
	private GameController gc;

	// Use this for initialization
	void Awake () {

		gc = GameController.gc;
		if (gc == null)
			Instantiate (gameController);
	}
}

//good luck!