using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	private GameObject nuxController;

	private string[] credits;

	private CanvasGroup cg;

	public Text main;
	public Text shadow;

	// Use this for initialization
	void Start () {
	
		cg = GetComponent<CanvasGroup> ();

		nuxController = GameObject.Find ("HiddenNuxController");
		if (nuxController != null && !nuxController.GetComponent<HiddenNuxController>().nuxMode)
			credits = new string[] {"Minigame Designers\nM.A.N.N. Games", "Graphic Designer\nNick Huey", "Lead Programmer\nNick Sohm", "Controller Specialist\nMatthew Farr", "Master Physicist\nAbdullah Alradadi", "Lead Leader\nDr. Andrew M. Nuxoll", 
				"Apologies to Those\nHarmed in the Making:", "Nux, Crenshaw, Vegdahl,\nParachute Guy", "Great Job!"};
		else // make fun of nuxMode users
			credits = new string[] {"Minigame Designers\nM.A.N.N. Games", "Graphic Designer\nNick Huey", "Lead Programmer\nNick Sohm", "Controller Specialist\nMatthew Farr", "Master Physicist\nAbdullah Alradadi", "Lead Leader\nDr. Andrew M. Nuxoll", 
				"Apologies to Those\nHarmed in the Making:", "Nux, Crenshaw, Vegdahl,\nParachute Guy", "...You Cheated"};

		StartCoroutine (fader ());
	}

	private IEnumerator fader() {

		for (int j = 0; j < credits.Length; j++) { // for each string in the array

			yield return new WaitForSeconds(0.5f);

			main.text = credits[j]; // set the text
			shadow.text = credits[j];

			for (float i = 0; i <= 1; i+= 0.01f) { // go from invisible to visible

				cg.alpha = i;
				yield return new WaitForSeconds (0.01f);
			}

			cg.alpha = 1;
			
			yield return new WaitForSeconds (2.5f); // stay there for a bit

			for (float i = 1; i >= 0; i-= 0.01f) { // go from invisible to visible

				cg.alpha = i;
				yield return new WaitForSeconds (0.01f);
			}

			cg.alpha = 0;
		}
	}
}