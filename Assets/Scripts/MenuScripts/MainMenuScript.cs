using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public GameObject levelSelectMenu;

	public void OpenLevelSelect() {
		levelSelectMenu.SetActive(true);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (levelSelectMenu.activeInHierarchy) {
                levelSelectMenu.SetActive(false);
			} 
		}
	}

}
