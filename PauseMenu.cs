using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public static bool isPaused = false;

	public GameObject thePlayer;



	void Start()
	{
		//pauseMenu.SetActive(false);

	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("escape pressed");
            if(isPaused)
			{
				Debug.Log("resumed");
				ResumeGame();

			}
			else
			{
				Debug.Log("paused");
				PauseGame();
			}
		}
	}
	
	public void PauseGame()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
		isPaused = true;

		thePlayer.GetComponent<FirstPersonController>().enabled = false;

	}

	public void ResumeGame()
	{

		
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		isPaused = false;
		thePlayer.GetComponent<FirstPersonController>().enabled = true;

		Debug.Log("Resuming");
	}
	

	public void Settings()
	{

	}

	public void QuitGame()
	{
		Debug.Log("Quitting Game");
		Application.Quit();
	}
}
