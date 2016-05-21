using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public float sizeIncrement;

	public int speedUpOrDown;
	public float speed;
	public float speedIncrement;

	public bool potatoPickedUp;

	public bool p1;

	public int score;

	public AudioClip pickUp;
	
	private AudioSource aSource;

	private Rigidbody rb;
	
	void Start () {

		aSource = GetComponent<AudioSource> ();

		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		if (p1) { // movement for ball 1

			float moveHorizontal = Input.GetAxis ("FruitHorizontal1");
			float moveVertical = Input.GetAxis ("FruitVertical1");
		
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
			rb.AddForce (movement * speed);
		} 

		else if (!p1) {

			float moveHorizontal = Input.GetAxis ("FruitHorizontal2");
			float moveVertical = Input.GetAxis ("FruitVertical2");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			
			rb.AddForce (movement * speed);
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) // on picking up something
		{

			aSource.clip = pickUp;
			aSource.Play ();

			if (other.gameObject.name == "GoldPotato")
				potatoPickedUp = true; // for hiddenNux

			score++; // increment my score

			Destroy (other.gameObject); // get rid of it

			transform.localScale = new Vector3(transform.localScale.x + sizeIncrement, transform.localScale.y + sizeIncrement, transform.localScale.z + sizeIncrement); // set scale
			speed += speedIncrement * speedUpOrDown; // set speed (speedUpOrDown: 1 = speed goes up, -1 = speed goes down
		}
	}
}