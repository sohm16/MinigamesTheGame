using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public Sprite on;
	public Sprite off;

	public string gateName;

	private bool isOn;
	private bool onlyOnce;

	void Update() {

		if (isOn && !onlyOnce) {

			GetComponent<SpriteRenderer> ().sprite = on; // change sprite

			GameObject.Find (gateName).SetActive (false); // set off the corresponding gates

			onlyOnce = true;
		} 
		else if (!isOn)
			GetComponent<SpriteRenderer> ().sprite = off;
	}

	void OnTriggerEnter(Collider other) { // check to see if player or bananrang hits

		if (other.tag == "Player" || other.tag == "pickUp")
			isOn = true;
	}
}
