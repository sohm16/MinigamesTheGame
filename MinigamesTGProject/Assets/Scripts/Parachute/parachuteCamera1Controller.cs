using UnityEngine;
using System.Collections;

public class parachuteCamera1Controller : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
	
	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate ()
	{
		if(!parachuteGameManager.instance.hitSea1)
		transform.position = player.transform.position + offset;
	}
}