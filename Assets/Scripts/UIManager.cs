using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public static int score;
	public static int ammo;

	public Text score_text;
	public Text ammo_text;

	public GameObject character;
	CharShootingScript controlscript;


	void Awake ()
	{
		score = 0;
		ammo = 10;

		controlscript = character.GetComponent<CharShootingScript> ();
	}

	public void addScore(int increment) {
		score+= increment;
	}

	void Update ()
	{
		score_text.text = "Score: " + score;
		ammo_text.text = "Ammo: " + controlscript.getAmmo();
	}
}
