using UnityEngine;
using System.Collections;

public class BlockDropper : MonoBehaviour {

	private GameController gc;

	private bool p1Hit;	// use these to check to see if players are hit individually or at the same time as each other
	private bool p2Hit;
	private bool onlyOnce;
	private bool codeDone;

	private AudioSource landSound;

	private GameObject player1; // reference to player object because friggin random null for no reason
	private GameObject player2;

	private Animator nuxAnim; // ref to animators on players bc yes
	private Animator vegdahlAnim;

	// Use this for initialization
	void Start () {
	
		gc = GameController.gc;

		player1 = GameObject.Find ("NuxPlatformer");
		player2 = GameObject.Find ("VegdahlPlatformer"); // get objects

		landSound = GetComponent<AudioSource> ();

		vegdahlAnim = player2.GetComponent<Animator> ();
		nuxAnim = player1.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Application.loadedLevelName != "THANKYOU" && !onlyOnce && codeDone && !gc.overrideTimer) { // super ultra mega check to make sure not overlapping gameovers

			if (p1Hit || p2Hit) { // if either player is hit these things are gonna happen
				onlyOnce = true;
				gc.overrideTimer = true;
				gc.gameTime = 0;
				player1.GetComponentInChildren<JumpChecker>().gameOver = true;
				player2.GetComponentInChildren<JumpChecker>().gameOver = true;

			}

			if (p1Hit && !p2Hit) { // p1 hit only
				gc.gameOver (2);
				vegdahlAnim.SetTrigger ("Win");
				nuxAnim.SetTrigger ("Lose");
			}
			else if (!p1Hit && p2Hit) { // p2 hit only
				gc.gameOver (1);
				vegdahlAnim.SetTrigger ("Lose");
				nuxAnim.SetTrigger ("Win");
			}
			else if (p1Hit && p2Hit) { // both p's hit
				gc.gameOver (0);
				nuxAnim.SetTrigger ("Lose");
				vegdahlAnim.SetTrigger ("Lose");
			}

		}
	}

	void OnTriggerEnter(Collider other) {

		codeDone = false;
		landSound.Play ();

		if (other.gameObject == player1 && !p1Hit) { // stop the thing from dropping and scale down player for comedic effect
			
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			player1.transform.localScale = new Vector2 (player1.transform.localScale.x, player1.transform.localScale.y / 2);
			p1Hit = true;
		}
		
		if (other.gameObject == player2 && !p2Hit) { // same
			
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			player2.transform.localScale = new Vector2 (player2.transform.localScale.x, player2.transform.localScale.y / 2);
			p2Hit = true;
		}

		codeDone = true;

		if (other.gameObject.tag == "Floor")	// start a delay so you feel accomplished when you successfully dodge block
			StartCoroutine (destroyDelay ());
	}

	private IEnumerator destroyDelay() {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		yield return new WaitForSeconds (1f);
		if (Application.loadedLevelName != "THANKYOU") // for credits
			Destroy (gameObject);
	}
}
