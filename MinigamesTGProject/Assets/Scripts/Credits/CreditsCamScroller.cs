using UnityEngine;
using System.Collections;

public class CreditsCamScroller : MonoBehaviour {

	public float xEnd;
	public float yEnd;
	public float camIncrementXWise;
	public float camIncrementYWise;
	public float camPeriod;

	public GameObject nux;
	public GameObject veg;

	public GameObject fallingBlocks;

	public GameObject respawnPoint;

	// Use this for initialization
	void Start () {
	
		StartCoroutine (moveCam());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator moveCam() {

		for (float i = transform.localPosition.y; i > yEnd; i-= camIncrementYWise) { // go down until you hit the designated stop point
			
			transform.localPosition = new Vector3(transform.localPosition.x, i, transform.localPosition.z);
			yield return new WaitForSeconds (camPeriod);
		}

		for (float i = transform.localPosition.x; i < xEnd; i+= camIncrementXWise) { // go right until you hit the designated stop point

			transform.localPosition = new Vector3(i, transform.localPosition.y, transform.localPosition.z);
			yield return new WaitForSeconds (camPeriod);
		}




	}

	void OnTriggerExit(Collider other) { // for child boundary object, move it back to center if player gets out of bounds

		if (other.gameObject.name == "VegdahlPlatformer")
			other.gameObject.transform.position = respawnPoint.transform.position;
		else if (other.gameObject.name == "NuxPlatformer" || other.gameObject.name == "NuxPlatformer(Clone)")
			other.gameObject.transform.position = respawnPoint.transform.position;
		else if (other.gameObject.name == "EndPlatform")
			fallingBlocks.SetActive (true);
	}
}