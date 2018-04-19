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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
		gamePaused = false;
	}

	public void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
		gamePaused = true;
	}

	public void ExitGame() {
		Application.Quit();
	}
}
