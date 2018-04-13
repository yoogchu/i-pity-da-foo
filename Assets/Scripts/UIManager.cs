using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
		if (score > 25 && controlscript.getAmmo() != 0) 
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene ("WinScreen");
		} 
		else if (controlscript.getAmmo() <= 0) 
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene ("LoseScreen");
		} 
		else 
		{
			score_text.text = "SCORE: " + score;
			ammo_text.text = "AMMO: " + controlscript.getAmmo();
		}
		
	}
}
