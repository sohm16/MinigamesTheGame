using UnityEngine;
using System.Collections;

public class parachuteGameManager : MonoBehaviour {

	public static parachuteGameManager instance = null;
	public int time;
	public int score1;
	public int score2;
	public bool hitSea1;
	public bool hitSea2;

	private GameController gc;
	private bool onlyOnce;
	private GameObject target1;
	private GameObject target2;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		target1 = GameObject.Find ("Target1");
		target2 = GameObject.Find ("Target2");
		target1.transform.position += new Vector3 (Random.Range (-250f, 250f), 0, 0);
		//target2.transform.position += new Vector3 (Random.Range (-250f, 250f), 0, 0);
	}


	// Use this for initialization
	void Start () {
		gc = GameController.gc;
		gc.startGame (time);
		gc.timeBeforeTransition = 2;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (gc.gameTime <= 0 && !onlyOnce) {
			onlyOnce = true;
			if (score1 > score2)
				gc.gameOver (1);
			else if (score1 < score2) 
				gc.gameOver (2);
			else if (score1 == score2)
				gc.gameOver (0);
		}
	}
}
