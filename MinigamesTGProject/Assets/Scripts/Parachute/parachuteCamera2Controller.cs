using UnityEngine;
using System.Collections;

public class parachuteCamera2Controller : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
	
	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate ()
	{
		if(!parachuteGameManager.instance.hitSea2)
			transform.position = player.transform.position + offset;
	}
}