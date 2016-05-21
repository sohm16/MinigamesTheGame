using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingBlockController : MonoBehaviour {

	private GameController gc;

	private List<string> fallingList;
	private string[] fallingArray;

	public GameObject falling1;
	public GameObject falling2;
	public GameObject falling3;

	public GameObject player1;
	public GameObject player2;

	public float fallSpeed;

	private bool onlyOnce;

	public int time;

	private int waveNumber = 1;

	// Use this for initialization
	void Start () {
	
		fallingArray = new string[] {"falling1", "falling2", "falling3"}; // y cant u have lists of gameobjects tho
		fallingList = new List<string> (fallingArray);
		gc = GameController.gc;
		gc.startGame (time);

		StartCoroutine (dropBlocks()); // make it rain
	}

	void Update () {

		if (gc.gameTime <= 0 && !gc.overrideTimer && !onlyOnce) { // super ultra mega check to make sure no overlapping gameovers
			onlyOnce = true;
			gc.gameOver (0);
			player1.GetComponentInChildren<JumpChecker>().gameOver = true;
			player2.GetComponentInChildren<JumpChecker>().gameOver = true;
		}

	}

	private IEnumerator dropBlocks() { // while the minigame's goin, drop blocks if there isnt one already (or after one dies)

		while (gc.gameTime > 0) {

			if (fallingList.Count <= 0)
				fallingList = new List<string> (fallingArray); // refresh list (random cycles like minigames)

			if (GameObject.FindGameObjectWithTag ("Respawn") == null && !gc.overrideTimer) { // there aren't anymore falling ones

				int rand = Random.Range (0,fallingList.Count);
				GameObject projectile;

				if (fallingList[rand] == "falling1")
					projectile = falling1;
				else if (fallingList[rand] == "falling2")
					projectile = falling2;
				else
					projectile = falling3; // because you cant make a list of gameobjects apparently.. Y

				GameObject shot = (GameObject)Instantiate (projectile, new Vector3 (transform.position.x, transform.position.y + 3.5f, transform.position.z), transform.rotation);

				if(waveNumber > 1)
					fallSpeed = fallSpeed + 0.8f;

				shot.GetComponent<Rigidbody>().velocity = new Vector2(0, -fallSpeed); // adjust shot velocity

				waveNumber = waveNumber + 1;
				//samp


				//samp

				fallingList.RemoveAt (rand);
			}

			yield return new WaitForSeconds(0.1f); // dont crash pls ty
		}

	}
}
