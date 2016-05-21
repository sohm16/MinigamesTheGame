using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 5f, ball.transform.position.z - 3f);
	}
}
