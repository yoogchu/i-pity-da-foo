using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool gamePaused = false;

	public GameObject pauseMenuObject;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {

			if (gamePaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void Resume() {
		pauseMenuObject.SetActive(false);
		Time.timeScale = 1f;
		gamePaused = false;
	}

	public void Pause() {
		pauseMenuObject.SetActive(true);
		Time.timeScale = 0f;
		gamePaused = true;
	}

	public void ExitGame() {
		Application.Quit();
	}
}
