using UnityEngine;
using System.Collections;

public class bowlingCameraController : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
	
	void Start ()
	{
		offset = transform.position - new Vector3(player.transform.position.x, player.transform.position.y - 0.025f, player.transform.position.z - 0.025f);
	}
	
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}