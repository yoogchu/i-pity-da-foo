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
	CharShootingScript shootScript;
    CharControlScript controlScript;

    CanvasGroup crosshairManager;

	void Awake ()
	{
		score = 0;
		ammo = 10;

        shootScript = character.GetComponent<CharShootingScript> ();
        controlScript = character.GetComponent<CharControlScript>();
        crosshairManager = GetComponentInChildren<CanvasGroup>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void addScore(int increment) {
		score+= increment;
	}

	void Update ()
	{
		if (score > 25 && shootScript.getAmmo() >= 0) 
		{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene ("WinScreen");

		} 
		else if (shootScript.getAmmo() <= 0) 
		{
            ammo_text.text = "AMMO: " + shootScript.getAmmo();
            StartCoroutine(SlowMoAndWait());
		} 
		else 
		{
			score_text.text = "SCORE: " + score;
			ammo_text.text = "AMMO: " + shootScript.getAmmo();
		}

        if (controlScript.isAiming)
        {
            crosshairManager.alpha = 1;
        } else
        {
            crosshairManager.alpha = 0;
        }
	}

    IEnumerator SlowMoAndWait()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(3.0f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        if (score > 25)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");

        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
        }
    }
}
