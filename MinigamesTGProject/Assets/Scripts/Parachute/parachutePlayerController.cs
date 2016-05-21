using UnityEngine;
using System.Collections;

public class parachutePlayerController : MonoBehaviour {

	public float speed;

	public bool nuxCylinder = false;

	private Rigidbody rb;
	private bool landed;
	private bool landedOnTarget;
	private GameObject parachute;
	private GameObject spine;
	private GameObject l_shoulder;
	private GameObject r_shoulder;
	private bool onlyOnce;

	private Quaternion endLShoulderRotation;
	private Quaternion endRShoulderRotation;
	// Use this for initialization
	

	void Start () {
		rb = GetComponent<Rigidbody> ();
		parachute = GameObject.Find ("parachute");
		spine = GameObject.Find ("Spine");
		l_shoulder = GameObject.Find ("l_shoulder");
		r_shoulder = GameObject.Find ("r_shoulder");
		endLShoulderRotation = l_shoulder.transform.rotation * Quaternion.Euler(0, 0, 130);
		endRShoulderRotation = r_shoulder.transform.rotation * Quaternion.Euler(0, 0, 130);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (!landed) {
			float movementH = Input.GetAxis ("Player1Horizontal");
			float movementV = Input.GetAxis ("Player1Vertical");
			parachute.transform.eulerAngles = new Vector3 (movementV * 10, 0, -movementH * 10);
			spine.transform.eulerAngles = new Vector3 (movementV * 10, 0, -movementH * 10 - 90);
			Vector3 force = new Vector3 (movementH, 0, movementV) * speed;
			rb.AddForce (force);
			rb.position = new Vector3 
				(
					Mathf.Clamp (rb.position.x, -510, 510),  
					rb.position.y, 
					Mathf.Clamp (rb.position.z, -510, 510)
					);
		} 
		else if (landed) {
			parachute.transform.Translate(Vector3.up * Time.deltaTime*10);
			l_shoulder.transform.rotation = Quaternion.Slerp(l_shoulder.transform.rotation, endLShoulderRotation ,  Time.deltaTime*10);
			r_shoulder.transform.rotation = Quaternion.Slerp(r_shoulder.transform.rotation, endRShoulderRotation ,  Time.deltaTime*10);
		}




	}

	void OnCollisionEnter(Collision collision) {
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		landed = true;
	}
	void OnCollisionStay(Collision collision) {
		if (landedOnTarget && !onlyOnce){
			onlyOnce = true;
			rb.constraints = RigidbodyConstraints.FreezePosition;
			StartCoroutine (whatRing (collision.collider));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "landingArea") {
			landedOnTarget = true;
		}
		if (other.name == "sea") {
			parachuteGameManager.instance.hitSea1 = true;
		}
	}

	IEnumerator whatRing(Collider ring){
		yield return new WaitForSeconds(.75f);
		if (ring.tag == "ring0")
			parachuteGameManager.instance.score1 = 5;
		if (ring.tag == "ring1")
			parachuteGameManager.instance.score1 = 4;
		if (ring.tag == "ring2")
			parachuteGameManager.instance.score1 = 3;
		if (ring.tag == "ring3")
			parachuteGameManager.instance.score1 = 2;
		if (ring.tag == "ring4")
			parachuteGameManager.instance.score1 = 1;
		if (ring.tag == "ring5") {
			parachuteGameManager.instance.score1 = 5;
			nuxCylinder = true;
		}

	}
}
