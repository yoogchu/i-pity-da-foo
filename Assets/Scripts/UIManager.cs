
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
	public int win_condition;

	public GameObject character;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
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
		if (score >= win_condition && shootScript.getAmmo() >= 0) 
		{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene ("WinScreen");

		} 
		else if (shootScript.getAmmo() <= 0) 
		{
            ammo_text.text = "AMMO: " + shootScript.getAmmo() + "/" + ammo; 
            StartCoroutine(SlowMoAndWait());
		} 
		else 
		{
			score_text.text = "SCORE: " + score;
			ammo_text.text = "AMMO: " + shootScript.getAmmo() + "/" + ammo;

			if (score >= win_condition * (0.3)) {
				// fill in 1 star
                star1.SetActive(true);
			}
            
            if (score >= win_condition * (0.667)) {
				// fill in 2 stars
                star2.SetActive(true);
			}
            
            if (score >= win_condition - 2) {
                // fill in 3 stars
                star3.SetActive(true);
            }
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

		if (score > win_condition)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");

        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen");
        }
    }
}
