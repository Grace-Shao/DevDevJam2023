using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
		AudioManager.Instance.PlayMusic("Menu Theme");
    }
    public void LoadScene(int index)
	{
		// load the next scene into scene manager
		SceneManager.LoadScene(index);
	}
	public void LoadScene(string name)
	{
		// load the next scene into scene manager
		SceneManager.LoadScene(name);
	}

	// if play button hit, play the game scene
	public void LoadNextScene()
	{
		// load the next scene into scene manager
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void PlayAgain()
	{
		// load the next scene into scene manager
		SceneManager.LoadScene(0);
	}

	public void QuitGame()
	{
		// load the next scene into scene manager
		Application.Quit();
	}
}
