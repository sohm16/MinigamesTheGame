using UnityEngine;
using System.Collections;

public class Dance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GetComponent<Animator> ().SetTrigger ("Win");
	}
}
