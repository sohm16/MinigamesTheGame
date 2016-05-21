using UnityEngine;
using System.Collections;

public class SnowballController : MonoBehaviour {

	public float movementSpeed;
	public float initPush;
	public bool p1;
	private Rigidbody rb;
	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public float baseVelocity;
	public float baseVelocityIncrement;

	// Use this for initialization
	void Start () {
	
		if (p1) { // pick a random level (only for p1 so its only spawning one level)
			int levelPicker = Random.Range(0,3);
			if (levelPicker == 0)
				level1.SetActive (true);
			else if (levelPicker == 1)
				level2.SetActive (true);
			else
				level3.SetActive (true);
		}

		rb = GetComponent<Rigidbody> ();
		rb.AddForce (0, 0, initPush); // start with a bit more speed than 0
	}
	
	// Update is called once per frame
	void Update () {
	
		if (rb.velocity.z < baseVelocity) // if you get stopped, start up again quicker
			rb.AddForce (0, 0, baseVelocityIncrement);

		if (p1) {
			rb.velocity = new Vector3 (Input.GetAxis ("HorizontalPlatformer1") * movementSpeed / 5, rb.velocity.y, rb.velocity.z);
			rb.AddForce(0, 0, Input.GetAxis ("FruitVertical1") * movementSpeed); // get vertical input for increase speed
		}

		else {
			rb.velocity = new Vector3 (Input.GetAxis ("HorizontalPlatformer2") * movementSpeed / 5, rb.velocity.y, rb.velocity.z);
			rb.AddForce(0, 0, Input.GetAxis ("FruitVertical2") * movementSpeed);
		}
	}
}
