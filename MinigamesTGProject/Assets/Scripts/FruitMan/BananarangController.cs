using UnityEngine;
using System.Collections;

public class BananarangController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		StartCoroutine (expire ());
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Enemies1" || other.gameObject.tag == "Enemies2") {
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag != "Player")
			Destroy (gameObject);
	}

	private IEnumerator expire() {

		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
