using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController: MonoBehaviour {

	public float timeBetweenScales;
	public float scaleRate;
	public float scaleMultiplier;
	public float minScaleDivider;

	public Text titleText;

	void Start () {	// make list and pick a title, update the text
	
		if (Application.loadedLevel == 1) {
			string[] titleList = new string[] {"Soup Collector", "Might Tester", "NuxollWare", 
											"M.A.N.N. GAMES", "Minigames the Game", 
											"Bill Foster and the Toaster of Destiny", 
											"Cabbage Crabs", "Idk", "Minipotatoes the Potato",
											"WarioHair", "MGOTYTGOTY", "Frying Pan: The Musical",
											"Galactic Debris University", "Blubber Dash",
											"Deoxyrhinomosaicflaccid", "The Adventures of Party Pirate Perry",
											"Broom Sweepers", "Chef Spicy and the Pepper of Time",
											"Umbrella Samurai", "Tulip Paladin: Knights of Fufu",
											"PenguinBoxers: Penguiao v Hailweather",
											"Try Hard Tom and the Bees from Hell", "Underwater Biscuit Heaving",
											"DJ SandTractor: the Movie", "Pitchfork Perfect: The Bale Out",
											"Tyrone Jackson, the Expert Potter", 
											"Wacky Waving Inflatable Tube Socks", "See-Gull: The Oyster Assassin",
											"Grumpy Windmill Fighters", "Pepperoni Shamans",
											"Starch Monks", "Paul, the Olympic Diver Ogre",
											"Mutant Intergalatic Furniture", "Learning with Friends",
											"JAY, WE ARE IN A DROUGHT"};

			titleText.text = titleList [Random.Range (0, titleList.Length)];
		}
		StartCoroutine (animateText ());
	}

	private IEnumerator animateText() { // make text scale up & down

		float minScale = titleText.transform.localScale.x / minScaleDivider;
		float maxScale = titleText.transform.localScale.x * scaleMultiplier;
		while (true) {
			for (float i = minScale; i < maxScale; i += scaleRate) {

				titleText.transform.localScale = new Vector3 (i, i, 0);
				yield return new WaitForSeconds (timeBetweenScales);
			}

			yield return new WaitForSeconds (timeBetweenScales);

			for (float i = maxScale; i > minScale; i-= scaleRate) {

				titleText.transform.localScale = new Vector3 (i, i, 0);
				yield return new WaitForSeconds (timeBetweenScales);
			}

			yield return new WaitForSeconds (timeBetweenScales);
		}
	}
}