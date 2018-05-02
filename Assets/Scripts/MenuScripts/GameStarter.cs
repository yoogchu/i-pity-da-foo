using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

	public void startGame() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("AlphaDemoFinal");
	}

	public void MainMenuScreen() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenu");
	}

	public void LevelOne() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Level 1");
	}

	public void LevelTwo() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Level 2");
	}

	public void LevelThree() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Level 3");
	}

	public void LevelFour() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Level 4");
	}

	public void LevelFive() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Level 5");
	}

}
